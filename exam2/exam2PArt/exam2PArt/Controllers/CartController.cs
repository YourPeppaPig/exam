using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam2PArt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam2PArt.Controllers
{
    public class CartController : Controller
    {
        private readonly Cart cart;

        public CartController( Cart cartService)
        {
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

    }
}
