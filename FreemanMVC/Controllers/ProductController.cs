﻿using Microsoft.AspNetCore.Mvc;
using FreemanMVC.Models;
using FreemanMVC.Models.ViewModels;

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

        public IActionResult List(string category, int productPage = 1) => View(
            new ProductsListViewModel { Products = repository.Products
                .Where(p=>category == null || p.Category == category)
                .OrderBy(p => p.ProductId)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
            PagingInfo = new PagingInfo { 
                CurrentPage = productPage, 
                ItemsPerPage = PageSize, 
                TotalItems = category == null ? repository.Products.Count() : repository.Products.Where(p => p.Category == category).Count()},
            CurrentCategory = category
            });
    }
}
