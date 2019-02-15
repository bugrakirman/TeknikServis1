using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TeknikServis.Models.Enums;

namespace TeknikServis.Models.ViewModels
{
    public class MalfunctionViewModel
    {
        public Types Type { get; set; }
        public BrandTypes BrandType { get; set; }
        public string AvatarPath { get; set; }
        public string Message { get; set; }
        public HttpPostedFileBase PostedFile { get; set; }
    }
}
