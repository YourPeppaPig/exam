using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam2PArt.Data;
using exam2PArt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam2PArt.Controllers
{
    [Authorize]
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public RestaurantController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_dbContext.Restaurants.ToList());
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpPost]
        public IActionResult Create(exam2PArt.Models.Restaurants restaurant)
        {
            return View(restaurant);
        }


        [Authorize(Roles = Constants.Admin)]
        public IActionResult Delete(int id)
        {
            var rest = _dbContext.Restaurants.FirstOrDefault(x => x.Id == id);
            if (rest != null)
            {
                _dbContext.Restaurants.Remove(rest);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var rest = _dbContext.Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);
            return View(rest);
        }
    }
}
