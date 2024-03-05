using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalogo.Data.Constants;
using NerdStore.Catalogo.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NerdStore.Catalogo.Data.Extensions
{
    public static class DbContextExtensions
    {
        public static void AddCatalogoData(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(ContextConstants.DB_CONNECTION_NAME) ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<CatalogoContext>(options =>
            {
                options.UseSqlServer(connectionString,
                  x =>
                  {
                      x.MigrationsHistoryTable("__EFMigrationsHistory");
                      x.MigrationsAssembly(typeof(CatalogoContext).Assembly.GetName().Name);
                  });
            });
           
        }

        public static void SeedCatalogoData(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CatalogoContext>())
                {
                    ArgumentNullException.ThrowIfNull(context, nameof(context));
                   
                    context.Database.Migrate();

                    if (!context.Produtos.Any())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {                                
								var categoriaCamisa = new Categoria("Camisas", 100);
								var categoriaCaneca = new Categoria("Canecas", 101);
								var categoriaAdesivo = new Categoria("Adesivos", 102);								
								var categoriaBone = new Categoria("Bones", 103);
								var categoriaSmartphone = new Categoria("Smartphone", 104);
								var categoriaIphone = new Categoria("Iphone", 105);

								var produtosEspeciais = new List<Produto> 
                                {
                                    new Produto("Camiseta Developer", "Camiseta 100% algodão", true, 99.00M, categoriaCamisa.Id, DateTime.Now, "Camiseta1.jpg", new Dimensoes(5,5,5)),
                                    new Produto("Camiseta Code", "Camiseta 100% algodão", true, 89.00M, categoriaCamisa.Id, DateTime.Now, "camiseta2.jpg", new Dimensoes(5,5,5)),
                                    new Produto("Caneca StarBugs", "Aliquam erat volutpat", true, 49.00M, categoriaCaneca.Id, DateTime.Now, "caneca1.jpg", new Dimensoes(5,5,5)),
                                    new Produto("Caneca Code", "Aliquam erat volutpat", true, 45.00M, categoriaCaneca.Id, DateTime.Now, "caneca2.png", new Dimensoes(5,5,5)),

                                };
                                
                                context.Produtos.AddRange(produtosEspeciais);
                                context.SaveChanges();

                                var produtosDestaque = new List<Produto>
                                {
                                    new Produto("IPhone*", "Aliquam erat volutpat *", true, 1998.00M, categoriaIphone.Id, DateTime.Now, "iphone.png", new Dimensoes(5,5,5)),
                                    new Produto("Samsung Galaxy S4*", "Aliquam erat volutpat *", true, 1199.00M, categoriaSmartphone.Id, DateTime.Now, "galaxy-s4.jpg", new Dimensoes(5,5,5)),
                                    new Produto("Samsung Galaxy Note*", "Aliquam erat volutpat *", true, 1289.00M, categoriaSmartphone.Id, DateTime.Now, "galaxy-note.jpg", new Dimensoes(5,5,5)),
                                    new Produto("Z1*", "Aliquam erat volutpat *", true, 1389.00M, categoriaSmartphone.Id, DateTime.Now, "Z1.png", new Dimensoes(5,5,5)),

                                };

                                context.Produtos.AddRange(produtosDestaque);
                                context.SaveChanges();

								var produtos = new List<Produto>
								{
									new Produto("IPhone", "Aliquam erat volutpat", true, 2998.00M, categoriaIphone.Id, DateTime.Now, "iphone.png", new Dimensoes(5,5,5)),
									new Produto("Samsung Galaxy S4", "Aliquam erat volutpat", true, 989.00M, categoriaSmartphone.Id, DateTime.Now, "galaxy-s4.jpg", new Dimensoes(5,5,5)),
									new Produto("Samsung Galaxy Note", "Aliquam erat volutpat", true, 1179.00M, categoriaSmartphone.Id, DateTime.Now, "galaxy-note.jpg", new Dimensoes(5,5,5)),
									new Produto("Z1", "Aliquam erat volutpat", true, 1089.00M, categoriaSmartphone.Id, DateTime.Now, "Z1.png", new Dimensoes(5,5,5)),

								};

								context.Produtos.AddRange(produtos);
								context.SaveChanges();

								transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                    
                }
            }
        }

        public static void SeedCatalogoDataFromScripts(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<CatalogoContext>())
                {
                    ArgumentNullException.ThrowIfNull(context, nameof(context));
                    if (context.Produtos.Any()) return;
                    context.Database.Migrate();

                    var assembly = typeof(DbContextExtensions).Assembly;
                    var files = assembly.GetManifestResourceNames();

                    var executedSeedings = context.SeedingEntries.ToArray();
                    var filePrefix = $"{assembly.GetName().Name}.Seedings.";
                    foreach (var file in files.Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                                              .Select(f => new
                                              {
                                                  PhysicalFile = f,
                                                  LogicalFile = f.Replace(filePrefix, String.Empty)
                                              })
                                              .OrderBy(f => f.LogicalFile))
                    {
                        if (executedSeedings.Any(e => e.Name == file.LogicalFile))
                            continue;

                        string command = string.Empty;
                        using (Stream stream = assembly.GetManifestResourceStream(file.PhysicalFile))
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                command = reader.ReadToEnd();
                            }
                        }

                        if (String.IsNullOrWhiteSpace(command))
                            continue;

                        using (var transaction = context.Database.BeginTransaction())
                        {
                            try
                            {
                                context.Database.ExecuteSqlRaw(command);
                                context.SeedingEntries.Add(new Entities.SeedingEntry() { Name = file.LogicalFile });
                                context.SaveChanges();
                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }

                    }
                }
            }
        }
        
    }
}
