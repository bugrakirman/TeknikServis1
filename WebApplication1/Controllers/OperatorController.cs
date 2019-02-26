using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class OperatorController : BaseController
    {
        // GET: Operator
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Malfunctions()
        {
            ViewBag.Malfunctions = Malfunctions();
            return View();
        }
    }
}