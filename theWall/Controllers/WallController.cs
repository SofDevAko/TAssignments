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
    public class WallController : Controller
    {
        [HttpGet]
        [Route("wall")]
        public IActionResult Wall()
        {
            string username = HttpContext.Session.GetString("username");
            int? userid = HttpContext.Session.GetInt32("id");
            
            if(username == null)
            {
                TempData["usererror"] = "You must login to go to the Wall!";
                return RedirectToAction("Index");
            }
            else
            {
                int id = (int)userid;
                string querymes = "SELECT messages.id as msgid, message, users_id, messages.created_at, messages.updated_at, users.id, firstname FROM messages LEFT JOIN users ON users.id = users_id ORDER BY created_at DESC";
                string querycom = "SELECT comments.id, comment, comments.users_id, messages_id, comments.created_at, comments.updated_at, users.id as usid, firstname FROM comments JOIN users ON users.id = users_id ORDER BY created_at DESC";
                ViewBag.messages = DbConnector.Query(querymes);
                ViewBag.comments = DbConnector.Query(querycom);
                ViewBag.posterror = TempData["error"];
                ViewBag.ActUserName = username;
                ViewBag.ActUserID = id;
                return View("theWall");
            }
            
        }
        [HttpPost]
        [Route("createmsg")]
        public IActionResult CreateMessage(string message)
        {
            if(message != "")
            {
                int userid = (int)HttpContext.Session.GetInt32("id");
                string querypost = $"INSERT INTO messages (message, users_id, created_at, updated_at) VALUES ('{message}', {userid}, NOW(), NOW())";
                DbConnector.Execute(querypost);
                return Redirect("wall");
            }
            else
            {
                TempData["error"] = "Please enter something to post!";
                return Redirect("wall");
            }
            

        }
        [HttpPost]
        [Route("createcmt")]
        public IActionResult CreateComment(string comment, int id)
        {
            if(comment != "")
            {
                int userid = (int)HttpContext.Session.GetInt32("id");
                string username = HttpContext.Session.GetString("username");
                
                string querypost = $"INSERT INTO comments (comment, users_id, messages_id, created_at, updated_at) VALUES ('{comment}', {userid}, {id} , NOW(), NOW())";
                DbConnector.Execute(querypost);
                return Redirect("wall");
            }
            else
            {
                TempData["error"] = "Please enter something to post!";
                return Redirect("wall");
            }
            

        }

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
