
using Helperland.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
            if (HttpContext.Session.GetInt32("userId")==null)
            {
                return RedirectToAction("Index", "Home", new { loginModal="true" });
            }
            else
            {
                var id = HttpContext.Session.GetInt32("userId");
                Models.User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
            }

            return PartialView();
        }
    }
}
