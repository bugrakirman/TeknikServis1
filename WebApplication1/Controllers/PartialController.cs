using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknikServis.Models.IdentityModels;
using static TeknikServis.BLL.Identity.MembershipTools;

namespace WebApplication1.Controllers
{
    public class PartialController : Controller
    {
        // GET: Partial
        public ActionResult ProfileIndex()
        {
            
                var id = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                if (id == null)
                {
                    User defaultUser = new User()
                    {
                        Name = " ",
                        Surname = " "
                    };
                }
                User user = NewUserManager().FindById(id);

                return PartialView("Partials/_PartialProfile", user);
            
        }
    }
}