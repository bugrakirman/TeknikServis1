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
        [StringLength(20, MinimumLength = 5, ErrorMessage = "en az 5 karakter olmali")]
        [Required]
        [Display(Name = "Parola")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola tekrar")]
        [Compare("Password", ErrorMessage = "Sifreler uyusmuyor")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        [StringLength(11, MinimumLength = 11, ErrorMessage = "en az 5 karakter olmali")]
        [Required]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }
        [StringLength(20)]
        [Required]
        [Display(Name = "Cİnsiyet")]
        public string Gender { get; set; }
        [StringLength(20)]
        [Required]
        [Display(Name = "Ülke")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "Sehir")]
        public string City { get; set; }
        [StringLength(20)]
        [Required]
        [Display(Name = "Adres")]
        public string Adress { get; set; }




        
    }
}
