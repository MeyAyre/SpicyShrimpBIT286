using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BIT285_SpicyShrimps.Models
{
    public class MathDbContext : DbContext
    {
        public MathDbContext() : base()
        {

        }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}
    