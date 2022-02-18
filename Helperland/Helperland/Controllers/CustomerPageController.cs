
using Helperland.Data;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
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
            Thread.Sleep(1500);
            var Zipcode = _db.Zipcodes.Where(x => x.ZipcodeValue == setup.PostalCode);
            if (Zipcode.Count() >0)
            {
                int cityId = Zipcode.FirstOrDefault().CityId;
                string cityName = _db.Cities.FirstOrDefault(x=> x.Id == cityId).CityName;
                CookieOptions cookiePostalcode = new CookieOptions();
                cookiePostalcode.Expires = DateTime.Now.AddMinutes(30);
                Response.Cookies.Append("postalCode", setup.PostalCode, cookiePostalcode);
                Response.Cookies.Append("city", cityName, cookiePostalcode);
                return Ok(Json("true"));
            }
            
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
                useradd.isDefault = add.IsDefault;
                Addresses.Add(useradd);
            }

            return new JsonResult(Addresses);
        }

        [HttpPost]
        public ActionResult AddNewAddress(UserAddress useradd)
        {

            int Id = (int)HttpContext.Session.GetInt32("userId");
            useradd.UserId = Id;
            useradd.IsDefault = false;
            useradd.IsDeleted = false;
            User user = _db.Users.Where(x => x.UserId == Id).FirstOrDefault();
            useradd.Email = user.Email;
            var result = _db.UserAddresses.Add(useradd);
            _db.SaveChanges();
            if (result != null)
            {
                return Ok(Json("true"));
            }

            return Ok(Json("false"));
        }

        [HttpPost]
        public ActionResult CompleteBooking(CompleteBooking complete)
        {
            int Id = (int)HttpContext.Session.GetInt32("userId");
            
            ServiceRequest add = new ServiceRequest();
            add.UserId = Id;
            add.ServiceId = Id;
            add.ServiceStartDate = complete.ServiceStartDate;
            add.ZipCode = complete.PostalCode;
            add.ServiceHourlyRate = 25;
            add.ServiceHours = complete.ServiceHours;
            add.ExtraHours = complete.ExtraHours;
            add.SubTotal = (decimal)(complete.ServiceHours + complete.ExtraHours);
            add.TotalCost = (decimal)(add.SubTotal * add.ServiceHourlyRate);
            add.Comments = complete.Comments;
            add.PaymentDue = false;
            add.PaymentDone = true;
            add.HasPets = complete.HasPet;
            add.CreatedDate = DateTime.Now;
            add.ModifiedDate = DateTime.Now;
            add.HasIssue = false;

            var result = _db.ServiceRequests.Add(add);
            _db.SaveChanges();

            UserAddress useraddr = _db.UserAddresses.Where(x => x.AddressId == complete.AddressId).FirstOrDefault();

            ServiceRequestAddress srAddr = new ServiceRequestAddress();
            srAddr.AddressLine1 = useraddr.AddressLine1;
            srAddr.AddressLine2 = useraddr.AddressLine2;
            srAddr.City = useraddr.City;
            srAddr.Email = useraddr.Email;
            srAddr.Mobile = useraddr.Mobile;
            srAddr.PostalCode = useraddr.PostalCode;
            srAddr.ServiceRequestId = result.Entity.ServiceRequestId;
            srAddr.State = useraddr.State;

            var srAddrResult = _db.ServiceRequestAddresses.Add(srAddr);
            _db.SaveChanges();

            if(complete.Cabinet == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 1;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
            if (complete.Oven == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 2;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
            if (complete.Window == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 3;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
            if (complete.Fridge == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 4;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
            if (complete.Laundry == true)
            {
                ServiceRequestExtra srExtra = new ServiceRequestExtra();
                srExtra.ServiceRequestId = result.Entity.ServiceRequestId;
                srExtra.ServiceExtraId = 5;
                _db.ServiceRequestExtras.Add(srExtra);
                _db.SaveChanges();
            }
           
            if(result != null && srAddrResult != null)
            {
                return Ok(Json(result.Entity.ServiceRequestId));
            }

            return Ok(Json("false"));
        }
    }
}
