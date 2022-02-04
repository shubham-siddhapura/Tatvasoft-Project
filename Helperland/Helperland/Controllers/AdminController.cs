using Helperland.Data;
using Helperland.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class AdminController : Controller
    {
        private readonly HelperlandContext _db;

        public AdminController(HelperlandContext db)
        {
            _db = db;
        }

        public IActionResult ServiceRequest()
        {
            var id = HttpContext.Session.GetInt32("userId");

            if (id != null)
            {
                User user = _db.Users.Find(id);
                if(user.UserTypeId == 3)
                {
                    TempData["Name"] = user.FirstName;
                    return PartialView();
                }
            }
            else if (Request.Cookies["userId"] != null)
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                if (user.UserTypeId == 3)
                {
                    TempData["Name"] = user.FirstName;
                    return PartialView();
                }
            }
            
            return RedirectToAction("Index", "Home", new { loginModal = "true" });
        }
    }
}
