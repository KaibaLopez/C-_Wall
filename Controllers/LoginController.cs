using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Wall.Models;
using System.Linq;

namespace Wall.Controllers
{

    public class LoginController : Controller
    {
        private wallContext dbContext;

        public LoginController(wallContext context)
        {
            dbContext = context;
        }
        // GET: /Home/
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult newUser(registerUser input)
        {
            if (ModelState.IsValid)
            {
                // If a User exists with provided email
                if (dbContext.Users.Any(u => u.email == input.email))
                {
                    // Manually add a ModelState error to the Email field, with provided error message
                    ModelState.AddModelError("email", "Email already in use!");
                    return View("Login");
                }
                //Password Hasher!
                PasswordHasher<registerUser> Hasher = new PasswordHasher<registerUser>();
                input.password = Hasher.HashPassword(input, input.password);

                input.created_at = DateTime.Now;
                input.updated_at = DateTime.Now;
                User user = new User();
                user.first_name = input.first_name;
                user.last_name = input.last_name;
                user.email = input.email;
                user.password = input.password;

                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                //Savein Session and redirect to Index.

                HttpContext.Session.SetInt32("uId", user.uId);
                return RedirectToAction("Index", "Home");
            }
            else
            {

                // Error Messages ONLY work with View method.
                return View("Login");
            }

        }
        [HttpPost]
        [Route("login")]
        public IActionResult UserLogin(LoginUser input)
        {
            Console.WriteLine(input.logemail);
            if (ModelState.IsValid)
            {
                // If inital ModelState is valid, query for a user with provided email
                var userInDb = dbContext.Users.FirstOrDefault(u => u.email == input.logemail);
                if (userInDb == null)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("logemail", "Invalid Email/Password");
                    return View("Login");
                }
                // Initialize hasher object
                var hasher = new PasswordHasher<LoginUser>();
                // varify provided password against hash stored in db
                var result = hasher.VerifyHashedPassword(input, userInDb.password, input.password);

                if (result == 0)
                {
                    // Add an error to ModelState and return to View!
                    ModelState.AddModelError("logemail", "Invalid Email/Password");
                    return View("Login");
                }
                // other code
                HttpContext.Session.SetInt32("uId", userInDb.uId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Console.WriteLine(" ################################################################### ");
                Console.WriteLine( " invalid because it's empty ");
                // Error Messages ONLY work with View method.
                return View("Login");
            }

        }
    }
}
