using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TeknikServis.Models.Entities;
using 

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ArizaKayit(Ariza model)
        {
            try
            {
                var userManager = NewUserManager();
                var user = await userManager.FindByIdAsync(model.UserProfileViewModel.Id);

                user.Name = model.UserProfileViewModel.Name;
                user.Surname = model.UserProfileViewModel.Surname;
                user.PhoneNumber = model.UserProfileViewModel.PhoneNumber;
                if (user.Email != model.UserProfileViewModel.Email)
                {
                    //todo tekrar aktivasyon maili gönderilmeli. rolü de aktif olmamış role çevrilmeli.
                }
                user.Email = model.UserProfileViewModel.Email;

                if (model.UserProfileViewModel.PostedFile != null &&
                    model.UserProfileViewModel.PostedFile.ContentLength > 0)
                {
                    var file = model.UserProfileViewModel.PostedFile;
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
                    user.AvatarPath = "/Upload/" + fileName + extName;
                }


                await userManager.UpdateAsync(user);
                TempData["Message"] = "Güncelleme işlemi başarılı";
                return RedirectToAction("UserProfile");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index","Home");
            }
        }
    }
}

