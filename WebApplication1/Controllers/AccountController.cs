﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TeknikServis.Models.IdentityModels;
using TeknikServis.Models.ViewModels;
using static TeknikServis.BLL.Identity.MembershipTools;

namespace WebApplication1.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            if (HttpContext.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Home");
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.CountryList = CountryList();
            return View();
        }

        [HttpPost]
        
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            ViewBag.CountryList = CountryList();
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }
            try
            {
                var userStore = NewUserStore();
                var userManager = NewUserManager();
                var roleManager = NewRoleManager();

                var user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("UserName", "daha önce alınmış");
                    return View("Register", model);
                }

                var newUser = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Adress = model.Adress,
                    BirthDate = model.BirthDate,
                    City = model.City,
                      Country=model.Country,
                       Gender=model.Gender,
                         PhoneNumber=model.PhoneNumber,
                          
                };
                var result = await userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    if (userStore.Users.Count()==1)
                    {
                        await userManager.AddToRoleAsync(newUser.Id, "Admin");
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newUser.Id, "User");
                    }
                    //to do  kullanıcıya mail
                }
                else
                {
                    var err = "";
                    foreach (var resultError in result.Errors)
                    {
                        err += resultError + " ";
                    }
                    ModelState.AddModelError("",err);
                    return View("Register", model);
                }
                TempData["Message"] = "kaydiniz alinmistir giris yapiniz";
                return RedirectToAction("Index","Account");
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"bir hata oluştu{ex.Message}",
                    ActionName = "Register",
                    ControllerName = "Account",
                    ErrorCode = 500
                };
                return RedirectToAction("Error","Home");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index",model);
                }
                var userManager = NewUserManager();
                var user = await userManager.FindAsync(model.UserName, model.Password);
                if (user==null)
                {
                    ModelState.AddModelError("", "kullanici adi veya sifre hatali");
                    return View("Index",model);
                }
                var authManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties()
                    {
                     IsPersistent=model.RememberMe
                },userIdentity);
                return RedirectToAction("Index","Home");
            }
            catch (Exception ex)
            {
                TempData["Model"] = new ErrorViewModel()
                {
                    Text = $"bir hata oluştu{ex.Message}",
                    ActionName = "Register",
                    ControllerName = "Account",
                    ErrorCode = 500
                };
                return RedirectToAction("Error", "Home");
            }
            
        }
        [HttpGet]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index","Account");
        }
    }
}