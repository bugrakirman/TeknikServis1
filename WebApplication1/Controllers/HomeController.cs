using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TeknikServis.BLL.Helpers;
using TeknikServis.BLL.Repositories;
using TeknikServis.Models.Entities;
using TeknikServis.Models.ViewModels;

namespace WebApplication1.Controllers
{

    
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Malfunction()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Malfunction(MalfunctionViewModel model)
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Add(MalfunctionViewModel model)
        {

            try
            {
                var data = new Malfunction()
                {
                    Message = model.Message,
                    Type = model.Type,
                    BrandType = model.BrandType
                };

                if (model.PostedFile != null &&
                    model.PostedFile.ContentLength > 0)
                {
                    var file = model.PostedFile;
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    string extName = Path.GetExtension(file.FileName);
                    fileName = StringHelpers.UrlFormatConverter(fileName);
                    fileName += StringHelpers.GetCode();
                    var klasoryolu = Server.MapPath("~/Upload/");
                    var dosyayolu = Server.MapPath("~/Upload/") + fileName + extName;

                    if (!Directory.Exists(klasoryolu))
                        Directory.CreateDirectory(klasoryolu);
                    file.SaveAs(dosyayolu);

                    WebImage img = new WebImage(dosyayolu);
                    img.Resize(250, 250, false);
                    img.AddTextWatermark("Wissen");
                    img.Save(dosyayolu);
                    data.AvatarPath = "/Upload/" + fileName + extName;
                    
                }

                

                var a = new MalfunctionRepo().Insert(data);

                TempData["Message"] = $"Kaydınız Alınmıştır";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
    }
}

