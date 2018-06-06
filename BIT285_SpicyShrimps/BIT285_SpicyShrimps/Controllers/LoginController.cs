using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BIT285_SpicyShrimps.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BIT285_SpicyShrimps.Controllers
{
    public class LoginController : Controller
    {
        //initializing the Mathdbcontext database with a controller
        private MathDbContext m;
        public LoginController(MathDbContext _m)
        {
            //switched the m's around 
            m = _m;
        }

        // Student login
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        //teacher login Get method
        [HttpGet]
        public ActionResult teacherLogin()
        {
            return View("TeacherLogin");
        }

        //student login Get method
        [HttpGet]
        public ActionResult studentLogin()
        {
            return View("StudentLogin");
        }
        //teacher login method, Post method
        [HttpPost]
        public ActionResult teacherLogin(Teacher teacher)
        {
            //check if the username and password matches
            Teacher current = m.Teachers.First((t) => t.Username == teacher.Username && t.Password == teacher.Password);
            if (current != null)
            {
                //new instance of the Activity class
                Activity teacherActivity = new Activity();
                //assigns the teacher's name as the username
                teacherActivity.ActivityName = "Username: " + current.Username;

                //Add teacherActivity
                m.Activities.Add(teacherActivity);
                //update database
                m.SaveChanges();
                //add the teacher to the session variable
                HttpContext.Session.SetString("Teacher", current.Username);
                return Redirect("/");
            }
            else
            {
                //error handling
                ViewBag["error"] = "Login failed. Could not find a teacher with that username and password";
                return View("TeacherLogin");
            }
        }
        //Method for student login that records the information about the student to store and display for the teacher
        [HttpPost]
        public ActionResult studentLogin(Student student)
        {
            //check if username and password are correct
            Student current = m.Students.First((s) => s.Username == student.Username && s.OneWordPassword == student.OneWordPassword);
            if (current != null)
            {

                //new instance of the Activity class
                Activity studentActivity = new Activity();
                //assigns the student's name as whatever firstname/last initial is entered
                studentActivity.ActivityName = "Choose your name: " + current.Username;
                //records the student's password for the teacher to see 
                studentActivity.ActivityPassword = "Your Password: " + current.OneWordPassword;


                //code for how long each level took to complete

                //Assigns the ActivityDate to the current date and time
                studentActivity.ActivityDate = DateTime.Now;

                //Add the student's current level
                //Cannot do until levels are created later
                //studentActivity.ActivityLevel = 

                //Add userActivity
                m.Activities.Add(studentActivity);
                //update database
                m.SaveChanges();
                //add the student to the session variable
                HttpContext.Session.SetString("Student", current.Username);
                return Redirect("/");
            }
            else
            {
                //error handling
                ViewBag["error"] = "Login failed. Could not find a student with that username and password";
                return View("StudentLogin");
            }
        }

        //method for logging out
        //this is pretty similar to the log in method, except it removes
        //the user
        public ActionResult Logout()
        {
            //since the students and teachers will be stored in a session variable when they log in, as they log out their session will be clear
            HttpContext.Session.Clear();
            return View("Index");

            //the code below is if we want to record info about the student as they are logging out

            ////creates a new session variable called student
            //string username = HttpContext.Session.GetString("username");
            //if(string.IsNullOrEmpty(username))
            //{
            //    return View("Index");
            //}
            //Student student = m.Students.Where(s => s.Username == username).First();
            //Activity studentActivity = new Activity();
            //studentActivity.ActivityName = "Logout: " + student.Username;
            //studentActivity.ActivityDate = DateTime.Now;
            //m.Activities.Add(studentActivity);
            //m.SaveChanges();
            //return View("Index");

            
        }

        //adding new students

        public ActionResult Create()
        {

            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            //if there aren't any users with a username that matches 
            //the saved usernames, then add the user
            if (!m.Students.Any((s) => s.Username == student.Username))
            {
                //making the level of student 0 at time of creation
                student.Level = 0;
                m.Students.Add(student);
             
                m.SaveChanges();
                return Redirect("/");
            }
            else
            {
                //error handling message
                ViewBag["error"] = "User already exists";
                return View("Create");
            }
        }

        //Deleting students

        public ActionResult Delete()
        {

            return View("Delete");
        }

        [HttpPost]
        public ActionResult Delete(Student student)
        {
            ///if the student exists in the database, delete the student
            if (m.Students.Any((s) => s.Username == student.Username))
            {

                m.Students.Remove(student);

                m.SaveChanges();
                return Redirect("/");
            }
            else
            {
                //error handling message
                ViewBag["error"] = "Student doesn't exist";
                return View("Delete");
            }
        }

        //purpose: the startpage where the student will choose to start the game
        //where they left off or to start from the beginning
        //preconditions: student was directed to this page
        //postconditions: to return the View of this page
        [HttpGet]
        public ActionResult Start()
        {
            return View();
        }
    //TODo for the following to work Set up database

        //purpose: the startpage where the student will choose to start the game
        //where they left off or to start from the beginning
        //preconditions:
        //postconditions: the student will either be directed to the level where
        //they last were or to the start of the game
        [HttpPost]
        public ActionResult Start(string submit)
        {
            //getting the session variable and storing it in a string
            string name = HttpContext.Session.GetString("Student");
           // making a student equal to the session student
            Student current  = m.Students.Single(s => s.Username == name);
            //making a new activity to store the info in
            Activity start = new Activity();
            start.ActivityDate = DateTime.Now;
            start.ActivityID = current.StudentID;
            start.ActivityName = current.Username;
            start.ActivityPassword = current.OneWordPassword;
            //switch to select which level to redirect to 
            switch (submit)
            {
                //the strings are the name of the submit boxes in the view
                case "startOver":

                    start.Level = 0;
                    start.LevelCompletionTime = DateTime.Now - DateTime.Now;
                    m.Activities.Add(start);
                    m.SaveChanges();
                    //go to the level 1 game name of view is LevelOne
                    return View("LevelOne", start);//RedirectToAction("LevelOne", start);

                case "startPrevious":
                      start.Level = current.Level;
                      start.LevelCompletionTime = DateTime.Now - DateTime.Now;
                    //the activity is sent to start the levelcompletion time
                    //redirecting to whatever the view names are for the levels
                    //Names of the levels 
                    //could do if or switch statements to get level and redirect to the name of the level
                    return RedirectToAction(actionName: "Level" + current.Level, routeValues: start);
                default:
                    throw new Exception();
                    //break;
            }
        }

        //purpose: Level one of the game
        //preconditions: student was directed to this page
        //postconditions: to return the View of this page
        [HttpGet]
        public ActionResult LevelOne(Activity a)
        {
     
             a.Level = 1;
            a.LevelCompletionTime = DateTime.Now - DateTime.Now;
            DateTime startTime = DateTime.Now;
           
            //something is wron
            //Database has not been set up for this one yet so can't save changes 
           //  m.SaveChanges();
            return View(a);
        }
        //purpose: Level one of the game will change the time to the time it takes
        // the student to complete 
        //preconditions: student was directed to this page, 
        //postconditions: to 
        [HttpPost]
        public ActionResult LevelOne(Activity a, string jewel)
        {
            //ToDo 
            //jewel is the name of the jewel shape that will determine if got right jewel
            //to go on to nextlevel
            //if (jewel == rightJewel)
            
            DateTime dt = Convert.ToDateTime(a.LevelCompletionTime.ToString());
            DateTime time = DateTime.Now;
            a.LevelCompletionTime = time.Subtract(dt);
            m.SaveChanges();
            return RedirectToAction("Level2");
        }


        //purpose: to calculate the time it takes to complete a level
        //precondition:
        //postcondition: the time it takes to complete a method will be calculated
        private TimeSpan CalculateTime(DateTime DT1)
        {
            DateTime DT2 = DateTime.Now;
            TimeSpan time = DT2.Subtract(DT1);
            Console.WriteLine(time);
            return time;
        }

    }
}
