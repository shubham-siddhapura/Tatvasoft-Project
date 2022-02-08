
using Helperland.Data;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Helperland.Controllers
{
    public class CustomerPageController : Controller
    {
        private readonly HelperlandContext _db;

        public CustomerPageController(HelperlandContext db)
        {
            _db = db;
        }

        public IActionResult CustServiceHistory()
        {
            if (HttpContext.Session.GetInt32("userId")!=null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                Models.User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                if (user.UserTypeId == 1)
                {
                    return PartialView();
                }
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                if (user.UserTypeId == 1)
                {
                    return PartialView();
                }
            }

            return RedirectToAction("Index", "Home", new { loginModal = "true" });
            
        }

        

        public IActionResult BookService()
        {
            

            return PartialView();
        }

        [HttpPost]
        public IActionResult BookService(SetupService setup)
        {

            return PartialView();
        }

        [HttpPost]
        public IActionResult BookService(ScheduleService schedule)
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult BookService(DetailService detail)
        {
            return PartialView();
        }
    }
}
