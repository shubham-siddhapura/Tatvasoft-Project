
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
                User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();

                List<CustomerDashbord> dashbord = new List<CustomerDashbord>();

                var table = _db.ServiceRequests.Where(x => x.UserId == id && x.Status == 2).ToList();

                foreach(var data in table)
                {
                    
                    CustomerDashbord sr = new CustomerDashbord();
                    sr.ServiceRequestId = data.ServiceRequestId;

                    sr.ServiceStartDate = data.ServiceStartDate.ToString("dd/MM/yyyy");
                    sr.StartTime = data.ServiceStartDate.ToString("HH:mm");
                    sr.EndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm tt");

                    sr.TotalCost = data.TotalCost;

                    if (data.ServiceProviderId != null) {

                        User sp = _db.Users.Where(x => x.UserId == data.ServiceProviderId).FirstOrDefault();

                        sr.ServiceProvider = sp.FirstName + " " + sp.LastName;

                        decimal rating = _db.Ratings.Where(x => x.RatingTo == data.ServiceProviderId).Average(x => x.Ratings);

                        sr.SPRatings = rating;

                    }

                    dashbord.Add(sr);
                }             

                if (user.UserTypeId == 1)
                {
                    return PartialView(dashbord);
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
                User user = _db.Users.Find(id);
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
            string date = complete.ServiceStartDate.ToString("dd/MM/yyyy")+ " "+ complete.ServiceTime;
            add.ServiceStartDate = DateTime.Parse(date);
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

            /* status
             * 0 : Completed
             * 1 : Cancelled
             * 2 : Pending             
             */

            add.Status = 2;
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

        [HttpPost]
        public IActionResult CancelServiceRequest(ServiceRequest cancel)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                
                ServiceRequest cancelService = _db.ServiceRequests.FirstOrDefault(x=>x.ServiceRequestId == cancel.ServiceRequestId);
                cancelService.Status = 1;
                if (cancel.Comments != null)
                {
                    cancelService.Comments = cancel.Comments;
                }
                    
                var result = _db.ServiceRequests.Update(cancelService);
                _db.SaveChanges();
                if(result != null)
                {
                    return Ok(Json("true"));
                }
            }
            return Ok(Json("false"));
        }

        [HttpPost]
        public IActionResult RescheduleServiceRequest(CustomerDashbord reschedule)
        {
            ServiceRequest rescheduleService = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == reschedule.ServiceRequestId);

            string date = reschedule.ServiceStartDate + " " + reschedule.StartTime;

            rescheduleService.ServiceStartDate = DateTime.Parse(date);
            rescheduleService.ServiceRequestId = reschedule.ServiceRequestId;

            var result = _db.ServiceRequests.Update(rescheduleService);
            _db.SaveChanges();

            if (result != null)
            {
                return Ok(Json("true"));
            }

            return Ok(Json("false"));
        }

        [HttpGet]
        public JsonResult DashbordServiceDetails(CustomerDashbord ID)
        {

            CustomerDashbordServiceDetails Details = new CustomerDashbordServiceDetails();

            ServiceRequest sr = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == ID.ServiceRequestId);
            Details.ServiceRequestId = ID.ServiceRequestId;
            Details.Date = sr.ServiceStartDate.ToString("dd/MM/yyyy");
            Details.StartTime = sr.ServiceStartDate.ToString("HH:mm");
            Details.Duration = sr.SubTotal;
            Details.EndTime = sr.ServiceStartDate.AddHours((double)sr.SubTotal).ToString("HH:mm");
            Details.TotalCost = sr.TotalCost;
            Details.Comments = sr.Comments;

            List<ServiceRequestExtra> Extra = _db.ServiceRequestExtras.Where(x => x.ServiceRequestId == ID.ServiceRequestId).ToList();

            foreach(ServiceRequestExtra row in Extra)
            {
                if(row.ServiceExtraId == 1)
                {
                    Details.Cabinet = true;
                }
                else if(row.ServiceExtraId == 2)
                {
                    Details.Oven = true;
                }
                else if(row.ServiceExtraId == 3)
                {
                    Details.Window = true;
                }
                else if(row.ServiceExtraId == 4)
                {
                    Details.Fridge = true;
                }
                else
                {
                    Details.Laundry = true;
                }
            }

            ServiceRequestAddress Address = _db.ServiceRequestAddresses.FirstOrDefault(x=>x.ServiceRequestId == ID.ServiceRequestId);

            Details.Address = Address.AddressLine1 + " " + Address.AddressLine2 + " "+ Address.PostalCode+ " " + Address.City;

            Details.PhoneNo = Address.Mobile;
            Details.Email = Address.Email;

            return new JsonResult(Details);
        }

    }
}

