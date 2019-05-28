using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam2PArt.Data;
using exam2PArt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam2PArt.Controllers
{
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public DishesController(ApplicationDbContext dbContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpGet]
        public IActionResult Create(int id)
        {
            return View(new Dishes { Id = id });
        }

        [Authorize(Roles = Constants.Admin)]
        [HttpPost]
        public IActionResult Create(Dishes dish)
        {
            var rest = _dbContext.Restaurants.FirstOrDefault(x => x.Id == dish.Id);
            rest.Dishes.Add(dish);
            _dbContext.SaveChanges();
            return RedirectToAction("Details", "Restaurant", new { id = rest.Id });

        }

        [Authorize(Roles = Constants.Admin)]
        public IActionResult Delete(int id, int restId)
        {
            var dish = _dbContext.DIshes.FirstOrDefault(x => x.Id == id);
            if (dish != null)
            {
                _dbContext.DIshes.Remove(dish);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Details", "Restaurant", new { id = restId });
        }

        [Authorize(Roles = Constants.User)]
        public async Task<IActionResult> Add(int id)
        {
            var dish = _dbContext.DIshes
                .Include(x => x.Restaurant)
                .FirstOrDefault(x => x.Id == id);
            if (dish == null) return RedirectToAction("Details", "Restaurant", new { id = dish.Restaurant.Id });

            var user = await _dbContext.Users
                .Include(x => x.Cart)
                .ThenInclude(x => x.Dishes)
                .FirstOrDefaultAsync(x => x.Id == _userManager.GetUserId(User));

            if (user.Cart == null || user.Cart.Restaurant.Id != dish.Restaurant.Id)
            {
                user.Cart = new Cart
                {
                    Dishes = new List<Dishes>
                {
                    dish
                },
                    Restaurant = dish.Restaurant
                };
            }

            else
            {
                user.Cart.Dishes.Add(dish);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Details", "Restaurant", new { id = dish.Restaurant.Id });
        }

    }
}
