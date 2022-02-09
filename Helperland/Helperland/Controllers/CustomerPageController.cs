
using Helperland.Data;
using Helperland.Models;
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
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                Models.User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                
            }
            else if (Request.Cookies["userId"] != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.UserId == Convert.ToInt32(Request.Cookies["userId"]));
                TempData["name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                
            }

            TempData["PostalCode"] = "";
            
            return PartialView();
        }

        [HttpPost]
        public IActionResult Validpost(SetupService setup)
        {
            var Zipcode = _db.Zipcodes.Where(x => x.ZipcodeValue == setup.PostalCode);
            if (Zipcode.Count() >0)
            {
                CookieOptions cookiePostalcode = new CookieOptions();
                cookiePostalcode.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("postalCode", setup.PostalCode, cookiePostalcode);
                TempData["PostalCode"] = setup.PostalCode;               
                return RedirectToAction("BookService", "CustomerPage", new { validPostcode="true"   });
            }
            TempData["wrongZipCode"] = "Postal code you have entered is not valid.";
            return RedirectToAction("BookService", "CustomerPage", new {wrongZip="true"});
        }

        [HttpPost]
        public IActionResult ScheduleService(ScheduleService schedule)
        {
            CookieOptions cookieScheduleService = new CookieOptions();
            cookieScheduleService.Expires = DateTime.Now.AddMinutes(30);
            Response.Cookies.Append("Date",Convert.ToString(schedule.Date), cookieScheduleService);
            Response.Cookies.Append("Time", Convert.ToString(schedule.Time), cookieScheduleService);
            Response.Cookies.Append("Duration", Convert.ToString(schedule.Duration), cookieScheduleService);
            Response.Cookies.Append("Fridge", Convert.ToString(schedule.extra.Fridge), cookieScheduleService);
            Response.Cookies.Append("Oven", Convert.ToString(schedule.extra.Oven), cookieScheduleService);
            Response.Cookies.Append("Laundry", Convert.ToString(schedule.extra.laundry), cookieScheduleService);
            Response.Cookies.Append("Window", Convert.ToString(schedule.extra.window), cookieScheduleService);
            Response.Cookies.Append("Cabinet", Convert.ToString(schedule.extra.Cabinet), cookieScheduleService);
            Response.Cookies.Append("Comments", schedule.Comments, cookieScheduleService);
            Response.Cookies.Append("havePet", Convert.ToString(schedule.havePet), cookieScheduleService);

            int? session = HttpContext.Session.GetInt32("userId");

            if (session == null)
            {
                if (ModelState.IsValid)
                {
                    string password = _db.Users.FirstOrDefault(x => x.Email == schedule.Email).Password;

                    bool pass = BCrypt.Net.BCrypt.Verify(schedule.Password, password);
                    if (_db.Users.Where(x => x.Email == schedule.Email && pass).Count() > 0)
                    {
                        var user = _db.Users.FirstOrDefault(x => x.Email == schedule.Email);

                        if (schedule.Remember == true)
                        {
                            CookieOptions cookieRemember = new CookieOptions();
                            cookieRemember.Expires = DateTime.Now.AddSeconds(60);
                            Response.Cookies.Append("userId", Convert.ToString(user.UserId), cookieRemember);
                        }

                        HttpContext.Session.SetInt32("userId", user.UserId);

                        if (user.UserTypeId == 1)
                        {
                            return RedirectToAction("BookService", "CustomerPage", new { details="true"});
                        }
                        else if (user.UserTypeId == 2)
                        {
                            cookieScheduleService.Expires = DateTime.Now.AddMilliseconds(10);
                            return RedirectToAction("SPUpcomingService", "ServicePro");
                        }
                        else if (user.UserTypeId == 3)
                        {
                             cookieScheduleService.Expires = DateTime.Now.AddMilliseconds(10);
                            return RedirectToAction("ServiceRequest", "Admin");
                        }

                    }
                    else
                    {
                        TempData["showAlert"] = "show alert";
                        TempData["loginFail"] = "Username and Password are invalid.";
                        return RedirectToAction("Index", "Home", new { loginFail = "true" });
                    }
                }
            }
            
            return RedirectToAction("BookService", "CustomerPage", new { details="true" });
        }

        [HttpPost]
        public IActionResult BookService(DetailService detail)
        {
            return PartialView();
        }
    }
}
