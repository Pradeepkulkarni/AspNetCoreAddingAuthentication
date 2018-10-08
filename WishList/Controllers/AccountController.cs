using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WishList.Models;
using WishList.Models.AccountViewModels;


namespace WishList.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            //set each of the read only field using parameter of the matching type
            _userManager = userManager;
            _signInManager = signInManager;

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
           
            var resutl = _userManager.CreateAsync(new ApplicationUser() {
                Email =model.Email,
                UserName = model.Email
            }, model.Password).Result;
            if(!resutl.Succeeded)
            {
                foreach(var error in resutl.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }
    }
}
