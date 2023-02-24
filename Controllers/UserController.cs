using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sigin = Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myfreela.context;
using myfreela.viewmodels;
using Microsoft.EntityFrameworkCore;
using myfreela.Models;
using Microsoft.AspNetCore.Authorization;

namespace myfreela.Controllers
{   

    public class UserController : Controller
    {
        private readonly UserManager<User> _user;
        private readonly SignInManager<User> _sigInManager;

        public UserController(UserManager<User> user, SignInManager<User> signInManager)
        {
            _user = user;
            _sigInManager = signInManager;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {   
                var userDB = await _user.FindByEmailAsync(userViewModel.Email!);
                
                if (userDB != null)
                {
                    ModelState.AddModelError("Email","Já existe um usuário com este email");
                    return View(userViewModel);
                }
                userDB = new User();
                MaperViewModelToDbModel(userViewModel,userDB);
                var result = await _user.CreateAsync(userDB,userViewModel.Password!);

                if (result.Succeeded)
                {   
                    return View();
                }
                foreach (var erro in result.Errors)
                {
                    ModelState.AddModelError("",erro.Description);
                }
                return View();
            }
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {    
                var resultLogin = await _sigInManager.PasswordSignInAsync(userViewModel.UserName!,userViewModel.Password!,false,false);

                if (resultLogin.Succeeded)
                {
                    return RedirectToAction("Index","Projects");
                }
                if (resultLogin == Sigin.SignInResult.Failed)
                {   
                    ModelState.AddModelError("","Falha ao logar");
                }else
                {   
                    ModelState.AddModelError("","Algum erro ocorreu"); 
                }     
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _sigInManager.SignOutAsync();

            return RedirectToAction(nameof(Index));
        }

        private void MaperViewModelToDbModel(RegisterViewModel userViewModel, IdentityUser<int> userDB)
        {
            userDB.Email = userViewModel.Email;
            userDB.UserName = userViewModel.UserName;
            userDB.EmailConfirmed = false;
            userDB.PhoneNumberConfirmed = false;
            userDB.TwoFactorEnabled = false;
            userDB.LockoutEnabled = false;
            userDB.AccessFailedCount = 0;
        }

    }
}