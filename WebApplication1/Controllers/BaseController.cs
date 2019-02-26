using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknikServis.BLL.Repositories;
using TeknikServis.Models.Entities;

namespace WebApplication1.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected List<SelectListItem> CountryList()
        {

            var list = new List<SelectListItem>()
            {
                new SelectListItem{Text="---Select---",Value="null"},
                new SelectListItem{Text="Türkiye",Value="TR"},
                new SelectListItem { Text = "Almanya", Value = "ALM" },
                new SelectListItem { Text = "İtalya", Value = "ITL" },
                new SelectListItem { Text = "İngiltere", Value = "ING" },
                new SelectListItem{Text="ABD",Value="AB"}

            };
            return list;
        }
        protected List<Malfunction> Malfunctions()
        {
            var malfunctions = new MalfunctionRepo().GetAll().OrderBy(x=>x.Type).ToList();
            var list = new List<Malfunction>();
            list.AddRange(malfunctions);
            return list;
        }
    }
}