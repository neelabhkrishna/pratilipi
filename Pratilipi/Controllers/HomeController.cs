using Pratilipi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pratilipi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // Sign Up GET Request
        [ActionName("SignUp")]
        public ActionResult SignUp()
        {
            return View();
        }
        // User Registration
        [HttpPost]
        public ActionResult SignUp(User obj)
        {
            // Check Model Validations
            if (ModelState.IsValid)
            {
                // Instance of Model Context class
                using(var db=new Model1())
                {
                    // Check Username is already exists
                    var validateusername = db.Users.Where(x => x.username == obj.username).Take(1).Any();
                    // If username is not exists then Insert User information into User Table
                    if (validateusername == false)
                    {
                        //Insert record
                        db.Users.Add(obj);
                        // Commit chanes
                       int result = db.SaveChanges();
                        // If Changes committed in database successfully then show message
                        if (result > 0)
                        {
                            return Content("<script>alert('User Added');location.href='/Home/Login'</script>");

                        }

                    }
                    else
                    {
                        // If username already exists
                        return Content("<script>alert('This username is already exists');location.href='/Home/SignUp'</script>");
                    }
                }
                return View();
            }
            else
            {
                return View();
            }
        }
        // User Login Page Open
        public ActionResult Login()
        {
            return View();
        }
        // Validate user credentials
        [HttpPost]
        public ActionResult Login(User obj)
        {
            // Check Model validation
            if (ModelState.IsValid)
            {
                // Instance of Model Context class

                using (var db = new Model1())
                {
                    // Check Username and password
                    var validateuser = db.Users.Where(x => x.username == obj.username & x.password == obj.password).FirstOrDefault();
                    if(validateuser!=null)
                    {
                        // Create username session and redirect to ViewStories page
                        Session["username"] = obj.username;
                        return RedirectToAction("ViewStories");
                    }
                    else
                    {
                        // Message on wrong credentials given by user
                        return Content("<script>alert('Invalid Username or password');location.href='/Home/Login'</script>");

                    }

                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult ViewStories()
        {
            // Check Username exists
            if (Session["username"] != null)
            {
                // Instance of Model Context class
                using (var db = new Model1())
                {
                    // Fetch all Stories from Database table Story
                    var data = db.Stories.ToList();
                    // Pass result to view
                    return View(data);
                }
            }
            else
            {
                // If username does not exists then redirect to Login
                return RedirectToAction("Login");
            }
        }
        public ActionResult View(int? id)
        {
            // When user clicks on any story , id will passed with url 
            if(id!=null & Session["username"] != null)
            {
                // Instance of Model context class
                using (var db = new Model1())
                {
                    var username = Session["username"].ToString();
                    // Check Whether user already seen same story
                    var checklogged = db.Loggeds.Where(x => x.username == username & x.Storyid == id).Take(1).Any();
                    // If not 
                    if (checklogged == false)
                    {
                        // SQL Parameter
                        SqlParameter storyid = new SqlParameter("@storyid", id);
                        // Update Views for Same same story id in Story Table
                        int result = db.Database.ExecuteSqlCommand("update Story set Views=(Views+1) WHERE id=@storyid", storyid);
                    }
                    // Update information in Logged table
                    Logged obj = new Logged
                    {
                        Storyid = id,
                        username = Session["username"].ToString(),
                        viewdate = DateTime.Now
                    };
                    // Add Information of user and his seen Story details in Logged table
                    db.Loggeds.Add(obj);
                    db.SaveChanges();
                 // Fetch Story as per Story id 
                    var data = db.Stories.Where(x => x.id == id).FirstOrDefault();
                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("ViewStories");
            }
        }
    }
}