﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknikServis.Models.IdentityModels;
using TeknikServis.Models.ViewModels;
using static TeknikServis.BLL.Identity.MembershipTools;

namespace WebApplication1.Controllers
{
    public class MyProfileController : Controller
    {
        // GET: MyProfile
        public ActionResult MyProfile()
        {
            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

            User user = NewUserManager().FindById(id);

            var newUser = new UpdateProfilePasswordViewModel()
            {
                ProfileViewModel = new ProfileViewModel()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber

                }

            };

            return View(newUser);
        }
        [HttpGet]
        public ActionResult ChangeProfile()
        {
            if (!ModelState.IsValid)
            {
                return View("MyProfile");
            }

            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();

            User user = NewUserManager().FindById(id);

            var newUser = new UpdateProfilePasswordViewModel()
            {
                ProfileViewModel = new ProfileViewModel()
                {
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName
                }
            };

            return View("MyProfile", newUser);
        }

        [HttpPost]
        public ActionResult ChangeProfile(UpdateProfilePasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View("MyProfile");
            }

            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var userManager = NewUserManager();
            User user = userManager.FindById(id);

            user.UserName = model.ProfileViewModel.UserName;
            user.Email = model.ProfileViewModel.Email;
            user.PhoneNumber = model.ProfileViewModel.PhoneNumber;

            var result = userManager.Update(user);

            if (result.Succeeded)
            {
                TempData["Message"] = "Profil Bilgileriniz Başarı ile Güncellenmiştir.";
                return RedirectToAction("MyProfile");
            }
            else
            {
                ModelState.AddModelError("", "Bir Hata Oluştu");
                return RedirectToAction("MyProfile", "Profile");
            }


        }
        [HttpPost]
        public ActionResult ChangePassword(UpdateProfilePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("MyProfile");
            }


            var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            User user = NewUserManager().FindById(id);

            var result = NewUserManager().ChangePassword(id, model.PasswordViewModel.OldPassword, model.PasswordViewModel.NewPassword);


            if (result.Succeeded)
            {
                TempData["Message"] = "Şifreniz Başarı ile Güncellenmiştir.";
                return RedirectToAction("LogOut", "Account");
            }
            else
            {

                ModelState.AddModelError("", "Eski şifreniz yanlıştır.");
                return View("MyProfile", model);
            }


        }
    }
}
