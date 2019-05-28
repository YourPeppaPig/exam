using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam2PArt.Data;
using exam2PArt.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam2PArt.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Restaurants restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return RedirectToAction("Index", "Restaurant");
        }
    }
}
