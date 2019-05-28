using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam2PArt.Data;
using exam2PArt.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam2PArt.Controllers
{
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DishesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            return View(await _context.DIshes.ToListAsync());
        }

        // GET: Dishes/Dish
        public async Task<IActionResult> Dish(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishes = await _context.DIshes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dishes == null)
            {
                return NotFound();
            }

            return View(dishes);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dishes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Dishes dish)
        {
                    _context.DIshes.Add(new Dishes
                    {
                        Id = dish.Id,
                        Name = dish.Name,
                        CostOfMeal = dish.CostOfMeal,
                        Description = dish.Description
                    });
                   
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(dish);
        }

        private bool DIshesExists(int id)
        {
            return _context.DIshes.Any(e => e.Id == id);
        }
    }
}
