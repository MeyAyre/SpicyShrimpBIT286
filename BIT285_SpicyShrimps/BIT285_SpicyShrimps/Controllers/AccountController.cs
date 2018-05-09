using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BIT285_SpicyShrimps.Models;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;


//For Teacher and Student Login.
//Student login records the time they signed on, and the last level they reached.
namespace BIT285_SpicyShrimps.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private MathDbContext m;
        public AccountController(MathDbContext _m)
        {
            _m = m;
        }

        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }

        //teacher login Get method
        [HttpGet]
        public ActionResult teacherLogin()
        {
            return View("Login");
        }

        //student login Get method
        [HttpGet]
        public ActionResult studentLogin()
        {
            return View("Login");
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
                return View("Login");
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

                //Assigns the ActivityDate to the current date and time
                studentActivity.ActivityDate = DateTime.Now;

                //Add the student's current level/gems collected
                //Cannot do until levels are created later
                //studentActivity.ActivityLevel = 


                //code for how long each level took to complete


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
                return View("Login");
            }
        }

        //method for logging out
        //this is pretty similar to the log in method, except it removes
        //the user
        public ActionResult Logout()
        {
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
       

        /**
         * Store temporary user in Session during account creation
         */
       
    }
}
