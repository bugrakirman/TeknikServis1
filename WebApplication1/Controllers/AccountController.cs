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
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.CountryList = CountryList();
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {

        //        return View("Index",model);
        //    }

        //    var userManager = NewUserManager();
        //    var roleManager = NewRoleManager();

        //    var rm = RegisterViewModel;

        //    var user = await userManager.FindByNameAsync(rm.UserName);
        //    if (user != null)
        //    {
        //        ModelState.AddModelError("UserName", "daha önce alınmış");
        //        return View("Index", model);
        //    }
        //}
    }
}