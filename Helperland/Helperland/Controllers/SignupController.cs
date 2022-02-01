using Helperland.Data;
using Helperland.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class SignupController : Controller
    {
        private readonly HelperlandContext _db;

        public SignupController(HelperlandContext db)
        {
            _db = db;
        }

        public IActionResult Signup()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {

            if (ModelState.IsValid)
            {
                if (_db.Users.Where(x => x.Email == user.Email).Count() == 0 && _db.Users.Where(x => x.Mobile == user.Mobile).Count() == 0)
                {
                    user.UserTypeId = 1;
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "User already exists";
                    return PartialView();
                }

            }
            return PartialView();
        }

        public IActionResult BecomeHelper()
        {
            return PartialView();
        }

   
        [HttpPost]
        public IActionResult BecomeHelper(User user)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.Where(x => x.Email == user.Email).Count() == 0 && _db.Users.Where(x => x.Mobile == user.Mobile).Count() == 0)
                {
                    user.UserTypeId = 2;
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "User already exists";
                    return PartialView();
                }

            }
            return PartialView();

        }
    }
}
