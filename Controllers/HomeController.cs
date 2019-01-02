using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Wall.Models;
using System.Linq;

namespace Wall.Controllers
{
    public static bool loggedIn(){
        int? v1 = HttpContext.Session.GetInt32("uId");

        return v1!=null;
    }
    public class HomeController : Controller
    {
        private wallContext dbContext;
        
        public HomeController(wallContext context){
            dbContext = context;
        }
        
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(loggedIn()){
                int _id = HttpContext.Session.GetInt32("uId") ?? default(int);
                ////Check session and redirect or use to login////////
                List<Message> allMessages = dbContext.Messages
                    .Include(post=>post.user)
                    .Include(cm => cm.comments)
                    .ThenInclude(u => u.user)
                    .OrderBy(d=>d.created_at)
                    .ToList();
                ViewBag._id = _id;

                return View(allMessages);
            }
            return RedirectToAction("Login","Login");
        }
        [HttpPost("postMessage")]
        public IActionResult postMessage(Message input)
        {
            if (ModelState.IsValid)
            {
                int? v1 = HttpContext.Session.GetInt32("uId");
                int _id = v1 ?? default(int);
                input.created_at= DateTime.Now;
                input.updated_at= DateTime.Now;
                input.uId = _id;

                
                
                dbContext.Messages.Add(input);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View("Index");
        }

        [HttpPost("postComment")]
        public IActionResult postComment(Comment input)
        {
            if (ModelState.IsValid)
            {
                int? v1 = HttpContext.Session.GetInt32("uId");
                int _id = v1 ?? default(int);
                
                input.uId = _id;
                input.created_at= DateTime.Now;
                input.updated_at= DateTime.Now;
                dbContext.Comments.Add(input);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index");
        }

        //Clear Methods!
        [HttpGet]
        [Route("clear")]
        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
