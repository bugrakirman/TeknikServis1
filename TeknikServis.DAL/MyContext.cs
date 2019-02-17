using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeknikServis.Models.Entities;

namespace TeknikServis.DAL
{
    public class MyContext : IdentityDbContext
    {
        public MyContext()
            : base("name=MyCon")
        {

        }

        public virtual DbSet<Malfunction> Categories { get; set; }
        
    }
}
