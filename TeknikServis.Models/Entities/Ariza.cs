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
    public class Ariza:BaseEntity<int>
    {
        [DisplayName("Ürün Tipi")]
        public Types Type { get; set; }
        public string AvatarPath { get; set; }
    }
}
