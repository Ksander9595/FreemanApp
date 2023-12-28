using Microsoft.AspNetCore.Mvc;
using FreemanMVC.Models;

namespace FreemanMVC.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public IActionResult List() => View(repository.Products);
    }
}
