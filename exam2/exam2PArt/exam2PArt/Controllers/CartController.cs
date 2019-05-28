using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam2PArt.Data;
using exam2PArt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam2PArt.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public CartController(ApplicationDbContext dbContext,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _dbContext.Users
                   .Include(x => x.Cart)
                   .FirstOrDefaultAsync(x => x.Id == _userManager.GetUserId(User));
            return View(user.Cart);
        }

        [HttpPost]
        public async Task<IActionResult> Order()
        {
            var user = await _dbContext.Users
                   .Include(x => x.Cart)
                   .ThenInclude(x => x.Restaurant)
                   .ThenInclude(x => x.Dishes)
                   .Include(x => x.Orders)
                   .FirstOrDefaultAsync(x => x.Id == _userManager.GetUserId(User));

            if (user.Orders == null)
            {
                user.Orders = new List<Order>();

            }
            user.Orders.Add(new Order
            {
                Restaurant = user.Cart.Restaurant,
                Dishes = user.Cart.Dishes,
                User = user
            });
            _dbContext.SaveChanges();

            return RedirectToAction();
        }
    }
}
