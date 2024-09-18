﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Data.Storage;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Catalogo.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueService _estoqueService;
        private readonly IMapper _mapper;
        private AzureStorageAccount _azureStorageAccount { get; set; }

        public ProdutoAppService(IProdutoRepository produtoRepository, 
                                 IMapper mapper, 
                                 IEstoqueService estoqueService
                                 ,AzureStorageAccount azureStorageAccount
            )
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _estoqueService = estoqueService;
            _azureStorageAccount = azureStorageAccount;
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterPorCategoria(int codigo)
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterPorCategoria(codigo));
        }

        public async Task<ProdutoViewModel> ObterPorId(Guid id)
        {
            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
        }

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
        }

        public async Task<IEnumerable<CategoriaViewModel>> ObterCategorias()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _produtoRepository.ObterCategorias());
        }

        public async Task<bool> AdicionarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);            
            _produtoRepository.Adicionar(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            var produto = _mapper.Map<Produto>(produtoViewModel);
            _produtoRepository.Atualizar(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<ProdutoViewModel> DebitarEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.DebitarEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao debitar estoque", HttpStatusCode.BadRequest);
            }

            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
        }

        public async Task<ProdutoViewModel> ReporEstoque(Guid id, int quantidade)
        {
            if (!_estoqueService.ReporEstoque(id, quantidade).Result)
            {
                throw new DomainException("Falha ao repor estoque", HttpStatusCode.BadRequest);
            }

            return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
            _estoqueService?.Dispose();
        }

        public async Task<bool> AdicionarProduto(ProdutoImagemViewModel produtoViewModel)
        {
            string urlImagem = await this._azureStorageAccount.UploadImage(produtoViewModel.ImagemBase64String);

            _produtoRepository.Adicionar(
                new Produto(produtoViewModel.Nome, produtoViewModel.Descricao, produtoViewModel.Ativo,
                        produtoViewModel.Valor, produtoViewModel.CategoriaId, produtoViewModel.DataCadastro,
                        urlImagem, new Dimensoes(produtoViewModel.Altura, produtoViewModel.Largura, produtoViewModel.Profundidade))
            );

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> AtualizarProduto(ProdutoImagemViewModel produtoViewModel)
        {
            throw new NotImplementedException();
        }
    }
}