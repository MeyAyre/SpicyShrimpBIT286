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

            // Look for any teachers, students and activities.
            if (context.Teachers.Any() && context.Students.Any() && context.Activities.Any())
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
            var students = new Student[]
            {
                new Student{FirstName="Joe",LastInitial="B",TeacherID=1,OneWordPassword="dog",Level=1,Activities="Joe.B" },

                new Student{FirstName="Jane",LastInitial="D",TeacherID=2,OneWordPassword="cat",Level=1,Activities="Jane.D" }

            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();
            //now seed the Activities
            var activities = new Activity[]
            {
                new Activity{ActivityName="Joe.B",ActivityPassword="dog",ActivityDate=DateTime.Now},
                new Activity{ActivityName="Jane.D",ActivityPassword="cat",ActivityDate=DateTime.Now},
            };
            foreach (Activity a in activities)
            {
                context.Activities.Add(a);
            }
            context.SaveChanges();

        }
    }
}
