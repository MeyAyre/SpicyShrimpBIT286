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
            _m = m;
        }

        // Student login
        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

       

        //student login Get methods
        [HttpGet]
        public ActionResult studentLogin()
        {
            return View("StudentLogin");
        }

        [HttpGet]
        public ActionResult VerifyTeacher()
        {
            return View("VerifyTeacher");
        }

        [HttpGet]
        public ActionResult ChooseName()
        {
            return View("ChooseName");
        }

        [HttpGet]
        public ActionResult SecretWord()
        {
            return View("SecretWord");
        }

        //teacher login Get methods
        [HttpGet]
        public ActionResult teacherLogin()
        {
            return View("TeacherLogin");
        }

        [HttpGet]
        public ActionResult TeacherDashboard()
        {
            return View("TeacherDashboard");
        }

        [HttpGet]
        public ActionResult AddStudents()
        {
            return View("AddStudents");
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
    }
}
