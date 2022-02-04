using Helperland.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class ServiceProController : Controller
    {

        private readonly HelperlandContext _db;

        public ServiceProController(HelperlandContext db)
        {
            _db = db;
        }

        public IActionResult SPUpcomingService()
        {
            int typeid = 0;
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                Models.User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                typeid = user.UserTypeId;
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                typeid = user.UserTypeId;
            }
            
            if (typeid == 2)
            {
                return PartialView();
            }
            return RedirectToAction("Index", "Home", new { loginModal = "true" });
            
        }
    }
}
