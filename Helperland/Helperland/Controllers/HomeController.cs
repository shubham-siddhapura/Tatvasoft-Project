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
using System.Net.Mail;

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
            
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            }
            
            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            
            return PartialView();
        }

        public IActionResult About()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            }

            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            
            return PartialView();
        }

        public IActionResult Contact()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            }

            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
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
                string serverFolder = "";
                if (contactu.Attach != null)
                {
                    string folder = "contactFiles/";
                    folder += Guid.NewGuid().ToString() + "_" + contactu.Attach.FileName;
                    serverFolder = Path.Combine(_webHostEnv.WebRootPath, folder);

                    FileStream files = new FileStream(serverFolder, FileMode.Create);
                    contactu.Attach.CopyToAsync(files);
                    contactu.FileName = folder;
                    files.Close();
                }
                contactu.CreatedOn = DateTime.Now;
                var contactData = _db.ContactUs.Add(contactu);
                _db.SaveChanges();

                List<string> emails = _db.Users.Where(x => x.UserTypeId == 3).Select(x => x.Email).ToList();

                string msg = "<p> user name :- " + contactu.FirstName + " " + contactu.LastName + " </p>" + 
                    "<p>mobile number :- " + contactu.PhoneNumber + " </p>" +"<p>email :- " + contactu.Email + "</p>" + "<p>query type :- " + contactu.Subject + "</p>" + "<p> message :- " + contactu.Message + "</p>" + "createdBy :- " + contactu.CreatedBy + "</p><p> createOn :- " + contactu.CreatedOn + "</p>";

                SendContactMail(msg, emails, serverFolder);
                return RedirectToAction("Index", "Home", new { msgSent = "true" });
            }
            return PartialView();

        }
        private static void SendContactMail(string msg, List<string> emails, string path)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.UseDefaultCredentials = true;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("siddshubham123456789@gmail.com", "Shubham@123");

            MailMessage message = new MailMessage();
            
                message.Subject = "New Message from Helperland";
                message.Body = msg;
            
            if(path != "")
            {
                Attachment attach = new Attachment(path);
                message.Attachments.Add(attach);
            }

            message.From = new MailAddress("siddshubham123456789@gmail.com");
            message.IsBodyHtml = true;
            foreach (string email in emails)
            {
                message.To.Add(email);
            }
            client.Send(message);
        }


        public IActionResult Prices()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            } 

            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
            }
            
            return PartialView();
        }

        public IActionResult Faq()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            }

            if (id != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == id);
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
