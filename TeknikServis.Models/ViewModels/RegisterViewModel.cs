using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Models.ViewModels
{
    public class RegisterViewModel
    {
        [StringLength(35)]
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [StringLength(35)]
        [Required]
        [Display(Name ="Soyad")]
        public string Surname { get; set; }
        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [StringLength(20,MinimumLength =5,ErrorMessage ="en az 5 karakter olmali")]
        [Required]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola tekrar")]
        [Compare("Password",ErrorMessage ="Sifreler uyusmuyor")]
        public string ConfirmPassword { get; set; }

    }
}
