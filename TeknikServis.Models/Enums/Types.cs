using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeknikServis.Models.Enums
{
    public enum Types
    {
        [Description("Bulasik")]
        BulasikMakinesi = 10,
        [Description("Buzdolabi")]
        Buzdolabi = 100,
        [Description("CamasirMakinesi")]
        CamasirMakinesi = 1000
    }
}
