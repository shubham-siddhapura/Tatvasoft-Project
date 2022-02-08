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
        [ResponseCache (Duration = 0)]
        public IActionResult SPUpcomingService()
        {
            
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                Models.User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                if (user.UserTypeId == 2)
                {
                    return PartialView();
                }

            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                if (user.UserTypeId == 2)
                {
                    return PartialView();
                }
            }
            
            return RedirectToAction("Index", "Home", new { loginModal = "true" });
            
        }
    }
}
