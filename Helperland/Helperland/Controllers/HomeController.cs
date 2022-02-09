using Helperland.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HelperlandContext _db;

        private readonly IWebHostEnvironment _webHostEnv;

        

        /*   public HomeController(ILogger<HomeController> logger)
           {
               _logger = logger;
           }
   */
        public HomeController(HelperlandContext db, IWebHostEnvironment webHostEnv)
        {
            _db = db;
            _webHostEnv = webHostEnv;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("userId");
            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            
            return PartialView();
        }

        public IActionResult About()
        {
            var id = HttpContext.Session.GetInt32("userId");
            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            return PartialView();
        }

        public IActionResult Contact()
        {
            var id = HttpContext.Session.GetInt32("userId");
            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            return PartialView();
        }

        [HttpPost]
        public IActionResult Contact(ContactU contactu)
        {

            if (ModelState.IsValid)
            {
                if (contactu.Attach != null)
                {
                    string folder = "contactFiles/";
                    folder += Guid.NewGuid().ToString() + "_" + contactu.Attach.FileName;
                    string serverFolder = Path.Combine(_webHostEnv.WebRootPath, folder);
                    contactu.Attach.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    contactu.FileName = folder;
                }
                contactu.CreatedOn = DateTime.Now;
                _db.ContactUs.Add(contactu);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home", new { msgSent = "true" });
            }
            return PartialView();

        }

        public IActionResult Prices()
        {
            var id = HttpContext.Session.GetInt32("userId");
            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            return PartialView();
        }

        public IActionResult Faq()
        {
            var id = HttpContext.Session.GetInt32("userId");
            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            else if(Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            return PartialView();
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
