
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

        [HttpPost]
        public ActionResult Validpost(SetupService setup)
        {
            var Zipcode = _db.Zipcodes.Where(x => x.ZipcodeValue == setup.PostalCode);
            if (Zipcode.Count() >0)
            {
                CookieOptions cookiePostalcode = new CookieOptions();
                cookiePostalcode.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("postalCode", setup.PostalCode, cookiePostalcode);
                TempData["PostalCode"] = setup.PostalCode;               
                return Ok(Json("true"));
            }
            TempData["wrongZipCode"] = "Postal code you have entered is not valid.";
            return Ok(Json("false"));
        }

        [HttpPost]
        public ActionResult ScheduleService(ScheduleService schedule)
        {
            if (ModelState.IsValid)
            {
                return Ok(Json("true"));
            }
            else
            {
                return Ok(Json("false"));
            }
            
        }

        
        [HttpGet]
        public JsonResult DetailsService()
        {

            List<DetailService> Addresses = new List<DetailService>();
            int? Id = HttpContext.Session.GetInt32("userId");
            string postalcode = Request.Cookies["postalCode"];
            var table = _db.UserAddresses.Where(x => x.UserId == Id && x.PostalCode == postalcode).ToList();
            
            foreach(var add in table)
            {
                DetailService useradd = new DetailService();
                useradd.AddressId = add.AddressId;
                useradd.AddressLine1 = add.AddressLine1;
                useradd.AddressLine2 = add.AddressLine2;
                useradd.City = add.City;
                useradd.PostalCode = add.PostalCode;
                useradd.Mobile = add.Mobile;

                Addresses.Add(useradd);
            }

            return new JsonResult(Addresses);
        }
    }
}
