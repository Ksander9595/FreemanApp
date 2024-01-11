using FreemanMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Http;
using FreemanMVC.Infrastructure;
using FreemanMVC.Models.ViewModels;


namespace FreemanMVC.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = GetCart(), ReturnUrl = returnUrl});
        }
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product? product = repository.Products.FirstOrDefault(p=>p.ProductId == productId);
            if(product != null)
            {
                Cart cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new {returnUrl});
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product? product = repository.Products.FirstOrDefault(p=>p.ProductId == productId);
            if (product != null)
            {
                Cart cart = GetCart();
                cart.RemoveItem(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl});
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart(); //если есть Корзина, возвращаем сеанс с Корзиной, инче создаем Корзину
            return cart;
        }
        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
