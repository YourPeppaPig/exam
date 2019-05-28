﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exam2PArt.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace exam2PArt.Controllers
{
    public class AccountController : Controller
    {
        readonly UserManager<IdentityUser> userManager;
        readonly RoleManager<IdentityRole> roleManager;
        readonly ApplicationDbContext context;

        public AccountController(Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ChangeRole()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            if (roleManager.Roles.Any(x => x.Name.ToLower() == "admin"))
                await roleManager.CreateAsync(new IdentityRole { Name = "admin" });
            if (roleManager.Roles.Any(x => x.Name.ToLower() == "user"))
                await roleManager.CreateAsync(new IdentityRole { Name = "user" });
            if (User.IsInRole("admin"))
            {
                await userManager.RemoveFromRoleAsync(user, "user");
                await userManager.AddToRoleAsync(user, "user");
            }
            else
            {
                await userManager.RemoveFromRoleAsync(user, "admin");
                await userManager.AddToRoleAsync(user, "admin");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
