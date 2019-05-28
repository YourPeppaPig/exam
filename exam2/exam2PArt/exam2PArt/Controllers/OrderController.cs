using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam2PArt.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam2PArt.Controllers
{
    public class OrderController : Controller
    {
        private readonly Cart _cart;

        public OrderController( Cart cartService)
        {
            _cart = cartService;
        }

        public IActionResult Result(double Prize)
        {
            return View(Prize);
        }
    }
}
