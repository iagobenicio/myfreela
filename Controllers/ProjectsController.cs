using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myfreela.Models;

namespace myfreela.Controllers
{   


    public class ProjectsController : Controller
    {
        private readonly UserManager<User> _userManager;
        public ProjectsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {   
            var user = await GetCurrentUser();

            if (user == null)
            {   
                return RedirectToAction("Index","User");
            } 
            return View();  

        }

        public async Task<User?> GetCurrentUser()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }
    }
}