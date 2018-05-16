using BIT285_SpicyShrimps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIT285_SpicyShrimps.Data
{
    public class DbInitializer
    {
        //purpose: to initialize seed data 
        public static void Initialize(MathDbContext context)
        {
            context.Database.EnsureCreated();
            //look for any students
            if (context.Students.Any())
            {
                //Db has been seeded
                return;
            }
            var students = new Student[]
            {
                new Student{}
            };
        }
    }
}
