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
            
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            }
            if (id != null)
            {
                User user = _db.Users.Find(id);
                if(user.UserTypeId == 3)
                {
                    TempData["Name"] = user.FirstName;
                    return PartialView();
                }
            }
            
            return RedirectToAction("Index", "Home", new { loginModal = "true" });
        }
    }
}
