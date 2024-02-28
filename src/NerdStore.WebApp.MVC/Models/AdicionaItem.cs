using Microsoft.AspNetCore.Mvc;
using System;

namespace NerdStore.WebApp.MVC.Models
{
    public class AdicionaItem
    {
        [FromRoute]
        public Guid Id { get; set; }

        [FromRoute]
        public int Quantidade { get; set; }
    }
}
