using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TeknikServis.BLL.Helpers;
using TeknikServis.BLL.Services.Senders;
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
                var userManager = NewUserManager();
                var userStore = NewUserStore();

                var isUserNameExist = await userStore.FindByNameAsync(model.UserName);

                if (isUserNameExist != null)
                {
                    ModelState.AddModelError("UserName", "Bu kullanıcı adı alınmıştır.");

                    return View("Register", model);
                }

                var newUser = new User()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    BirthDate = model.BirthDate.Date,
                    Gender = model.Gender,
                    Country = model.Country,
                    City = model.City,
                    Adress = model.Adress,
                    UserName = model.UserName,
                    ActivationCode = StringHelpers.GetCode()

                };

                var result = await userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {
                    if (userStore.Users.Count() == 1)
                    {
                        await userManager.AddToRoleAsync(newUser.Id, "Admin");

                    }
                    else
                    {
                        await userManager.AddToRoleAsync(newUser.Id, "User");
                    }

                    string SiteUrl = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host +
                                    (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);

                    var emailService = new EmailService();
                    var body = $"Merhaba <b>{newUser.Name} {newUser.Surname}</b><br>Hesabinizi aktif etmek icin asagidaki linke tiklayiniz<br> <a href='{SiteUrl}/Account/Activation?code={newUser.ActivationCode}'>Aktivasyon Linki</a>";
                    await emailService.SendAsync(new IdentityMessage() { Body = body, Subject = "Sitemize Hoşgeidiniz" }, newUser.Email);

                    TempData["Message"] = "Kaydınız başarı ile oluşturulmuştur.";

                    return RedirectToAction("Register");

                }
                else
                {
                    var err = "";
                    foreach (var error in result.Errors)
                    {
                        err += error + " ";
                    }

                    ModelState.AddModelError("", err);
                    return View("Register", model);
                }
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

                if (user.EmailConfirmed)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authManager.SignIn(new AuthenticationProperties()
                    {
                        IsPersistent = model.RememberMe
                    }, userIdentity);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception("Mailinize gelen aktivasyonu gerçekleştiriniz");
                }
                

                
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

        [HttpGet]
        public ActionResult Activation(string code)
        {
            try
            {
                var userStore = NewUserStore();
                var user = userStore.Users.FirstOrDefault(x => x.ActivationCode == code);

                if (user != null)
                {
                    if (user.EmailConfirmed)
                    {
                        ViewBag.Message = $"Bu hesap daha önce aktive edilmiştir";
                    }
                    else
                    {
                        user.EmailConfirmed = true;

                        userStore.Context.SaveChanges();
                        ViewBag.Message = $"Aktivasyon işleminiz başarılı";
                    }
                }
                else
                {
                    ViewBag.Message = $"Aktivasyon başarısız";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Aktivasyon işleminde bir hata oluştu";
            }

            return View();
        }
    }
}