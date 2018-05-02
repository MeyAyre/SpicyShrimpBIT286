using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIT285_SpicyShrimps.Models
{
    public class VisitorLogContext : DbContext
    {
     

        public VisitorLogContext() : base("name=VisitorLog")
        {
        }

        // Base DB models. Add a DbSet for any other entity type that you want to include in your model. 
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
    }

}



 
