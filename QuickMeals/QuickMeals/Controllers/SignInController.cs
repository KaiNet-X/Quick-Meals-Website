using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuickMeals.Models.Authentication;

namespace QuickMeals.Controllers
{
    public class SignInController : Controller
    {
        public SignInController(AuthenticationContext context)
        {
            AuthenticationHandler.context = context;
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            if (ModelState.IsValid)
            {
                if (!AuthenticationHandler.UserExists(user))
                {
                    AuthenticationHandler.CreateUser(user);
                    AuthenticationHandler.SignIn(HttpContext.Session, user);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("UserName", $"User with username {user.UserName} already exists");
            }    
            return View(user);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            if (AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            if (AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            if (AuthenticationHandler.UserExists(user))
            {
                if(AuthenticationHandler.PassedSignin(user))
                {
                    if(!AuthenticationHandler.UserSignedIn(user))
                    {
                        User dbInstance = AuthenticationHandler.GetDatabaseInstance(user);
                        AuthenticationHandler.SignIn(HttpContext.Session, dbInstance);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", $"User {user.UserName} is signed in on annother device.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", $"Password is incorrect");
                }
            }
            else
            {
                ModelState.AddModelError("UserName", $"User {user.UserName} does not exist.");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult SignOut()
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            ViewBag.User = AuthenticationHandler.CurrentUser(HttpContext.Session);
            return View();
        }
        [HttpPost]
        public IActionResult SignOut(bool f = false)
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            AuthenticationHandler.SignOut(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }
    }
}
