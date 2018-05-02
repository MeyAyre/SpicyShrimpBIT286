using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using BIT285_SpicyShrimps.Models;
//Release 1
//Log in
namespace BIT285_SpicyShrimps.Controllers
{
    public class AccountController : Controller
    {

        VisitorLogContext vl = new VisitorLogContext();

        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        //Method for recording the information about the user
        //to store and display
        [HttpPost]
        public ActionResult Login(User user)
        {
            User current = vl.Users.First((u) => u.Email == user.Email && u.Password == user.Password);
            if (current != null)
            {
                //new instance of the Activity class
                Activity userActivity = new Activity();
                //assigns the user's name as whatever email the user enters
                userActivity.ActivityName = "Login: " + current.Email;
                //gets the user's IP address and stores it
                userActivity.IpAddress = Request.UserHostAddress;
                //Assigns the ActivityDate to the current date and time
                userActivity.ActivityDate = DateTime.Now;
                //Add userActivity
                vl.Activities.Add(userActivity);
                //update database
                vl.SaveChanges();
                //add the user to the session variable
                Session.Add("User", current);
                return Redirect("/");
            }
            else
            {
                //error handling
                ViewBag["error"] = "Login failed. Could not find a user with that email and password";
                return View("Login");
            }
        }

        //method for logging out
        //this is pretty similar to the log in method, except it removes
        //the user
        public ActionResult Logout()
        {
            //creates a new session variable called user
            User user = (User)Session["User"];
            Activity userActivity = new Activity();
            userActivity.ActivityName = "Logout: " + user.Email;
            userActivity.IpAddress = Request.UserHostAddress;
            userActivity.ActivityDate = DateTime.Now;
            vl.Activities.Add(userActivity);
            vl.SaveChanges();
            Session.Remove("User");
            return View("Index");
        }

        public ActionResult Create()
        {

            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            //if there aren't any users with an email that matches 
            //the saved emails, then add the user
            if (!vl.Users.Any((u) => u.Email == user.Email))
            {

                vl.Users.Add(user);
                vl.SaveChanges();
                return Redirect("/");
            }
            else
            {
                //error handling message
                ViewBag["error"] = "User already exists";
                return View("Create");
            }
        }
        public ActionResult PasswordGenerator()
        {
            return View("PasswordGenerator");
        }
        [HttpPost]
        public ActionResult PasswordGenerator(ViewModels.PGViewModel pg, string submit)
        {
            //values from the ViewModel get passed in through the PGViewModel
            if (submit == "Generate")
            {
            }

            return View(pg);
        }


        /**
         * Store temporary user in Session during account creation
         */
        private User GetTempUser()
        {
            if (Session["tempUser"] == null)
            {
                Session["tempUser"] = new User();
            }
            return (User)Session["tempUser"];
        }

        private void RemoveTempUser()
        {
            Session.Remove("tempUser");
        }
    }

}