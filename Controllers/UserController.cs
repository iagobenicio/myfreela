using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myfreela.context;
using myfreela.viewmodels;

namespace myfreela.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _user;
        private readonly SignInManager<IdentityUser<int>> _sigInManager;

        public UserController(UserManager<IdentityUser<int>> user, SignInManager<IdentityUser<int>> signInManager)
        {
            _user = user;
            _sigInManager = signInManager;
        }
        
        [HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
        
            return View();

        }

        private IdentityUser<int> MaperViewModelToDbModel(RegisterViewModel userViewModel, IdentityUser<int> userDB)
        {
            userDB.Email = userViewModel.Email;
            userDB.EmailConfirmed = false;
            userDB.PhoneNumberConfirmed = false;
            userDB.TwoFactorEnabled = false;
            userDB.LockoutEnabled = false;
            userDB.AccessFailedCount = 0;

            return userDB;
        }
    }
}