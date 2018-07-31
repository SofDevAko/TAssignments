using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DbConnection;
using theWall.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace theWall.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.usererror = TempData["usererror"];
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginUser user)
        {
            TryValidateModel(user);
            if (ModelState.IsValid)
            {
                string checkq = $"SELECT * FROM users WHERE (email = '{user.email}')";
                var check = DbConnector.Query(checkq);
                if (check.Count == 0)
                {
                    ViewBag.result = "You have entered a wrong email!";
                    return View("Index");
                }
                else
                {
                    string comppass = check[0]["password"].ToString();
                    if (comppass == user.password)
                    {
                        int id = (int)check[0]["id"];
                        HttpContext.Session.SetInt32("id", id);
                        string name = (check[0]["firstname"]).ToString();
                        HttpContext.Session.SetString("username", name);
                        return Redirect("wall");
                    }
                    else
                    {
                        ViewBag.result = "You have entered a wrong password!";
                        return View("Index");
                    }
                }
            }
            else
            {
                return View("Index");
            }
            
            
        }
        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(RegisterUser newuser)
        {
            TryValidateModel(newuser);

            string checkq = $"SELECT * FROM users WHERE (email = '{newuser.email}')";
            var check = DbConnector.Query(checkq);
            if (ModelState.IsValid)
            {
                if (check.Count == 0)
                {
                    string query = $"INSERT INTO users (firstname, lastname, email, password, created_at, updated_at) VALUES ('{newuser.firstname}','{newuser.lastname}','{newuser.email}','{newuser.password}',NOW(),NOW())";
                    DbConnector.Execute(query);
                    var user = DbConnector.Query($"SELECT * FROM users WHERE (email = '{newuser.email}')");
                    int userid = (int)user[0]["id"];
                    string username = newuser.firstname;
                    HttpContext.Session.SetInt32("id", userid);
                    HttpContext.Session.SetString("username", username);
                    return RedirectToAction("Wall","Wall");
                }
                else
                {
                    ViewBag.result = "This email address is already in use!";
                    return View("Register");
                }
            }
            else
            {
                return View("Register");
            }
            
        }

        [HttpGet]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
