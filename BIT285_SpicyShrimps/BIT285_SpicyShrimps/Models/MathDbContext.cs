using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using System.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using BIT285_SpicyShrimps.Models;
using Microsoft.EntityFrameworkCore;

namespace BIT285_SpicyShrimps.Models
{
    public class MathDbContext : DbContext
    {
        public MathDbContext(DbContextOptions<MathDbContext> options) : base(options)
        {
        }
    
    public virtual DbSet<Teacher> Teachers { get; set; }
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Activity> Activities { get; set; }

}
}
    