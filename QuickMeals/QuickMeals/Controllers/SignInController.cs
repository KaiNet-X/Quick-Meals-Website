using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickMeals.Models;
using QuickMeals.Models.Authentication;

namespace QuickMeals.Controllers
{
    public class SignInController : Controller
    {
        //If signed in, return home. otherwise register view
        [HttpGet]
        public IActionResult Register()
        {
            Utilities.UserToView(this);

            if (AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");

            return View();
        }

        //Creates and signs in new user if user does not already exist. otherwise return the view with the user
        [HttpPost]
        public IActionResult Register(User user)
        {
            bool SignedIn = false;
            Utilities.UserToView(this, ref SignedIn);

            if (SignedIn)
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

        //If ont signed in, return signin view
        [HttpGet]
        public IActionResult SignIn()
        {
            bool SignedIn = true;
            Utilities.UserToView(this, ref SignedIn);

            if (SignedIn)
                return RedirectToAction("Index", "Home");

            return View();
        }

        //If user exists, sign in info are correct, and not signed in on annother device, user is signed in
        [HttpPost]
        public IActionResult SignIn(User user)
        {
            bool SignedIn = true;
            Utilities.UserToView(this, ref SignedIn);

            if (SignedIn)
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

        //if signed in, return the signout view
        [HttpGet]
        public IActionResult SignOut()
        {
            Utilities.UserToView(this);

            if (!AuthorizationHandler.IsSignedIn(HttpContext.Session))
                return RedirectToAction("Index", "Home");

            return View();
        }

        //If signed in, signs out the user. f is used just to overload the SignOut action method
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
