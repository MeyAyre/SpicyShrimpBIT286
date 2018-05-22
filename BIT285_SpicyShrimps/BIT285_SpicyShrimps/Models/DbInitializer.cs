//purpose: the purpose of this class is to seed the database with values
//it will check if a database is created and will create one if it is not

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIT285_SpicyShrimps.Models
{
    public class DbInitializer
    {
        //purpose: to seed the database with created values
        // will check if a database is created and will create one if not
        //preconditions: the context exists
        //postconditions: the database will be populated with the given values
        public static void Initialize(MathDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Teachers.Any())
            {
                return;   // DB has been seeded
            }

            var teachers = new Teacher[]
            {
                new Teacher{ FirstName="Nancy", LastName="Brown", Prefix="Ms", Password="now",
                ClassroomCode="001",Level="1" },
                 new Teacher{ FirstName="Pete", LastName="smith", Prefix="Mr", Password="then",
                ClassroomCode="001",Level="1" }
            };
            foreach (Teacher t in teachers)
            {
                context.Teachers.Add(t);
            }
    context.SaveChanges();
        }
    }
}
