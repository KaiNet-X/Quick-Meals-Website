using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuickMeals.Models;
using QuickMeals.Models.Authentication;

namespace QuickMeals.Controllers
{
    public class SignInController : Controller
    {
        public SignInController()
        {

        }
        [HttpGet]
        public IActionResult Register()
        {
            Utilities.UserToView(this);
            if (AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            Utilities.UserToView(this);
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
                ModelState.AddModelError("UserName", $"User with username {user.Username} already exists");
            }    
            return View(user);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            Utilities.UserToView(this);
            if (AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            Utilities.UserToView(this);
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
                        ModelState.AddModelError("UserName", $"User {user.Username} is signed in on annother device.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", $"Password is incorrect");
                }
            }
            else
            {
                ModelState.AddModelError("UserName", $"User {user.Username} does not exist.");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult SignOut()
        {
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            Utilities.UserToView(this);
            return View();
        }
        [HttpPost]
        public IActionResult SignOut(bool f = false)
        {
            Utilities.UserToView(this);
            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");
            AuthenticationHandler.SignOut(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }
    }
}
