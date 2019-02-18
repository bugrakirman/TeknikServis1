using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknikServis.Models.IdentityModels;

namespace WebApplication1.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.CountryList = CountryList();
            return View();
        }
        [HttpPost]
        public ActionResult Register(User model)
        {
            return View();
        }
    }
}