﻿using Microsoft.AspNetCore.Mvc;
using FreemanMVC.Models;

namespace FreemanMVC.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        public int PageSize = 4;
        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public IActionResult List(int productPage = 1) => View(repository.Products.OrderBy(p=>p.ProductId).Skip((productPage-1)*PageSize).Take(PageSize));
    }
}
