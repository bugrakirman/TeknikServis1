using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Models.Abstract;
using TeknikServis.Models.Enums;

namespace TeknikServis.Models.Entities
{
    [Table("Ariza")]
    public class Malfunction:BaseEntity<int>
    {
        [DisplayName("Ürün Tipi")]
        public Types Type { get; set; }
        [DisplayName("Marka")]
        public BrandTypes BrandType { get; set; }
        [DisplayName("Fotoğraf")]
        public string AvatarPath { get; set; }
        [DisplayName("Arıza Mesajı")]
        public string Message { get; set; }
    }
}
