
using Helperland.Data;
using Helperland.Models;
using Helperland.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mail;
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

        [HttpGet]
        public IActionResult CustServiceHistory()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            }
            if (id !=null)
            {
                
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

                        var rating = _db.Ratings.Where(x => x.RatingTo == data.ServiceProviderId);

                        if (rating.Count() > 0)
                        {
                            sr.SPRatings = Math.Round(rating.Average(x => x.Ratings),1);
                        }

                    }

                    dashbord.Add(sr);
                }             

                if (user.UserTypeId == 1)
                {
                    return PartialView(dashbord);
                }
            }
            
            return RedirectToAction("Index", "Home", new { loginModal = "true" });
            
        }

        
        public IActionResult BookService()
        {
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userId"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userid"]));
                id = HttpContext.Session.GetInt32("userId");
            }
            if (HttpContext.Session.GetInt32("userId") != null)
            {
                
                User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
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
            var table = _db.UserAddresses.Where(x => x.UserId == Id && x.PostalCode == postalcode && x.IsDeleted == false).ToList();
            
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
            add.ServiceProviderId = complete.ServiceProviderId;
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
                List<User> ServiceProviders = new List<User>();
                string msg = "";
                string subject = "";

                string url = Url.ActionLink("SPUpcomingService", "ServicePro");

                if (complete.ServiceProviderId == null)
                {
                    ServiceProviders = (from x in _db.Users join y in _db.FavoriteAndBlockeds on x.UserId equals y.TargetUserId into sp from y in sp.DefaultIfEmpty() where (x.UserTypeId == 2 && x.ZipCode == complete.PostalCode && y.TargetUserId == x.UserId && y.IsBlocked == false) || (x.UserTypeId == 2 && y == null) select x).ToList();

                    msg = "<p><b>Service Request Id: " + result.Entity.ServiceRequestId + "</b></p><p>New Service Request on the bord, go to New Service Request page by clicking below link</p>" + "<a href=" + url + ">Click Here</a>";

                    subject = "New Service Request in your area";
                }
                else
                {
                    ServiceProviders.Add(_db.Users.FirstOrDefault(x => x.UserId == complete.ServiceProviderId));
                    msg = "<p>There is Service Request in your area which is direct assigned to you by customer. Go and check it out.</p><p><b>Service Request Id: " + result.Entity.ServiceRequestId + "</b></p>" + "<a href=" + url + ">Click Here</a>";
                    subject = "Service request is direct assigned to you";
                }

                
                Task thread = Task.Run(() => SendMail(ServiceProviders, msg, subject));
                
                return Ok(Json(result.Entity.ServiceRequestId));
            }

            return Ok(Json("false"));
        }

        private static void SendMail(List<User> SPList, string msg, string subject)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.UseDefaultCredentials = true;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("siddshubham123456789@gmail.com", "Shubham@123");

            MailMessage message = new MailMessage();
            
                message.Subject = subject;
                message.Body = msg;
            
            message.From = new MailAddress("siddshubham123456789@gmail.com");
            message.IsBodyHtml = true;

            foreach (User user in SPList)
            {
                message.To.Add(user.Email);
            }
            client.Send(message);
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

                if(cancelService.ServiceProviderId != null)
                {
                    List<User> sp = _db.Users.Where(x => x.UserId == cancelService.ServiceProviderId).ToList();
                    string subject = "Service Request is Cancelled";
                    string msg = "Service Request " + cancelService.ServiceRequestId + " has been rescheduled by customer.";

                    Task thread = Task.Run(() => SendMail(sp, msg, subject));
                    
                }
            }
            return Ok(Json("false"));
        }

        [HttpPost]
        public IActionResult RescheduleServiceRequest(CustomerDashbord reschedule)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                ServiceRequest rescheduleService = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == reschedule.ServiceRequestId);

                string date = reschedule.ServiceStartDate + " " + reschedule.StartTime;

                rescheduleService.ServiceStartDate = DateTime.Parse(date);
                rescheduleService.ServiceRequestId = reschedule.ServiceRequestId;

                var result = _db.ServiceRequests.Update(rescheduleService);
                _db.SaveChanges();


                if (rescheduleService.ServiceProviderId != null)
                {
                    List<User> sp = _db.Users.Where(x => x.UserId == rescheduleService.ServiceProviderId).ToList();
                    string subject = "Service Request is Reschduled";
                    string msg = "Service Request " + rescheduleService.ServiceRequestId + " has been rescheduled by customer. New date and time are "+rescheduleService.ServiceStartDate.ToString("dd/MM/yyyy")+" "+rescheduleService.ServiceStartDate.ToString("hh:mm tt")+".";

                    Task thread = Task.Run(() => SendMail(sp, msg, subject));

                }

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
            Details.Status = sr.Status;

            if (sr.ServiceProviderId != null)
            {
                Details.CompletedService = _db.ServiceRequests.Where(x => x.ServiceProviderId == sr.ServiceProviderId && x.Status == 0).Count();

                User sp = _db.Users.Where(x => x.UserId == sr.ServiceProviderId).FirstOrDefault();

                Details.ServiceProviderId = sp.UserId;

                Details.ServiceProvider = sp.FirstName + " " + sp.LastName;

                var rating = _db.Ratings.Where(x => x.RatingTo == sr.ServiceProviderId);

                if (rating != null)
                {
                    Details.SPRatings = rating.Average(x => x.Ratings);
                }
            }

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

            Details.Address = Address.AddressLine1 + ", " + Address.AddressLine2 + ", "+ Address.City+ " - " + Address.PostalCode;

            Details.PhoneNo = Address.Mobile;
            Details.Email = Address.Email;

            return new JsonResult(Details);
        }

        /*====================== MY SETTING ========================*/

        [HttpGet]
        public JsonResult GetUserData()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                User user = _db.Users.FirstOrDefault(x=>x.UserId == Id);
                return new JsonResult(user);
            }
            return new JsonResult(false);
        }

        [HttpPost]
        public IActionResult UpdateUserData(User user)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                User updated = _db.Users.FirstOrDefault(x => x.UserId == Id);
                updated.FirstName = user.FirstName;
                updated.LastName = user.LastName;
                updated.Mobile = user.Mobile;
                updated.DateOfBirth = user.DateOfBirth;
                updated.ModifiedDate = DateTime.Now;
                var result = _db.Users.Update(updated);
                _db.SaveChanges();
                if(result != null)
                {
                    return Ok(Json("true"));
                }
            }
            return Ok(Json("false"));
        }

        [HttpGet]
        public JsonResult GetUserAddresses()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {

                List<UserAddress> Addresses = _db.UserAddresses.Where(x => x.UserId == Id && x.IsDeleted == false).ToList();
                return new JsonResult(Addresses);
            }

            return new JsonResult(false);

        }

        [HttpPost]
        public JsonResult DeleteUserAddress(UserAddress addr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                UserAddress delete = _db.UserAddresses.FirstOrDefault(x=>x.AddressId == addr.AddressId);

                delete.IsDeleted = true;
                var result = _db.UserAddresses.Update(delete);
                _db.SaveChanges();
                if(result != null)
                {
                    return new JsonResult(true);
                }
            }
            return new JsonResult(false);
        }

        /*----- Add User Address -----*/
        [HttpPost]
        public IActionResult AddUserAddress(UserAddress addr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                addr.UserId = (int)Id;
                addr.IsDefault = false;
                addr.IsDeleted = false;
                var result = _db.UserAddresses.Add(addr);
                _db.SaveChanges();
                if(result != null)
                {
                    return Ok(Json("true"));
                }
            }
            return Ok(Json("false"));
        }

        [HttpGet]
        public JsonResult GetAddressDataToModal(UserAddress addr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                UserAddress address = _db.UserAddresses.FirstOrDefault(x => x.AddressId == addr.AddressId);
                return new JsonResult(address);

            }
            return new JsonResult(false);
        }

        [HttpPost]
        public IActionResult EditUserAddress(UserAddress addr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                addr.UserId = (int)Id;
                var result = _db.UserAddresses.Update(addr);
                _db.SaveChanges();
                if(result != null)
                {
                    return Ok(Json("true"));
                }
                
            }
            return Ok(Json("false"));
        }

        /*-- change password --*/
        [HttpPost]
        public IActionResult ChangePassword(ChangePassword pwd)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == Id);
                if(BCrypt.Net.BCrypt.Verify(pwd.oldPassword, user.Password))
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(pwd.newPassword);
                    user.ModifiedDate = DateTime.Now;
                    _db.Users.Update(user);
                    _db.SaveChanges();
                    return Ok(Json("true"));
                }
                else
                {
                    return Ok(Json("wrong password"));
                }
            }
            return Ok(Json("false"));
        }

        /*---------- reschedule service ---- */
        [HttpGet]
        public JsonResult GetDateforReschedule(ServiceRequest req)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                ServiceRequest res = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == req.ServiceRequestId);

                return new JsonResult(res.ServiceStartDate);
            }
            return new JsonResult(false);
        }

        /*====== Service History ===========*/
        [HttpGet]
        public JsonResult GetServiceHistoryTable()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();

                List<CustomerDashbord> dashbord = new List<CustomerDashbord>();

                var table = _db.ServiceRequests.Where(x => x.UserId == id && x.Status != 2).ToList();

                foreach (var data in table)
                {

                    CustomerDashbord sr = new CustomerDashbord();
                    sr.ServiceRequestId = data.ServiceRequestId;

                    sr.ServiceStartDate = data.ServiceStartDate.ToString("dd/MM/yyyy");
                    sr.StartTime = data.ServiceStartDate.ToString("HH:mm");
                    sr.EndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm tt");

                    sr.Status = data.Status;

                    sr.TotalCost = data.TotalCost;

                    if (_db.Ratings.Where(x => x.ServiceRequestId == data.ServiceRequestId).Count() > 0)
                    {
                        sr.AlreadyRated = true;
                    }
                    else
                    {
                        sr.AlreadyRated = false;
                    }

                    if (data.ServiceProviderId != null)
                    {

                        User sp = _db.Users.Where(x => x.UserId == data.ServiceProviderId).FirstOrDefault();

                        sr.ServiceProviderId = sp.UserId;

                        sr.ServiceProvider = sp.FirstName + " " + sp.LastName;

                        var rating = _db.Ratings.Where(x => x.RatingTo == data.ServiceProviderId);

                        if(rating.Count()>0)
                        {
                            sr.SPRatings = rating.Average(x => x.Ratings);
                        }
                    }

                    dashbord.Add(sr);
                }
                return new JsonResult(dashbord);
            }
            return new JsonResult(false);
        }

        [HttpPost]
        public IActionResult RateServiceProvider(Rating rating)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                if(_db.Ratings.Where(x=>x.ServiceRequestId == rating.ServiceRequestId).Count() > 0)
                {
                    return Ok(Json("false"));
                }
                rating.RatingDate = DateTime.Now;
                ServiceRequest sr = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == rating.ServiceRequestId);
                rating.RatingTo = (int)sr.ServiceProviderId;
                rating.RatingFrom =(int)Id;

                var result = _db.Ratings.Add(rating);
                _db.SaveChanges();

                if(result != null)
                {
                    return Ok(Json("true"));
                }
            }
            return Ok(Json("false"));
        }

        /*====== GEt favourite pros =====*/
        [HttpGet]
        public JsonResult GetFavouritePros()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                List<int?> serviceProviders = _db.ServiceRequests.Where(x => x.UserId == Id && x.ServiceProviderId != null).Select(x=>x.ServiceProviderId).Distinct().ToList();

                List<CustomerFavouritePros> result = new List<CustomerFavouritePros>();

                foreach (int spId in serviceProviders)
                {
                    FavoriteAndBlocked isBlocked = _db.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == spId && x.TargetUserId == Id && x.IsBlocked == true);
                    if (isBlocked == null)
                    {
                        CustomerFavouritePros add = new CustomerFavouritePros();

                        add.UserId = (int)Id;
                        add.SpId = spId;

                        add.SpName = _db.Users.FirstOrDefault(x => x.UserId == spId).FirstName + " " + _db.Users.FirstOrDefault(x => x.UserId == spId).LastName;

                        add.Avatar = _db.Users.FirstOrDefault(x => x.UserId == spId).UserProfilePicture;

                        var ratings = _db.Ratings.Where(x => x.RatingTo == spId);

                        if (ratings.Count() > 0)
                        {
                            add.SpRatings = ratings.Average(x => x.Ratings);
                        }

                        add.Cleanings = _db.ServiceRequests.Where(x => x.ServiceProviderId == spId).Count();

                        FavoriteAndBlocked favOrBlock = _db.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == Id && x.TargetUserId == spId);

                        if (favOrBlock != null)
                        {
                            add.isBlock = favOrBlock.IsBlocked;
                            add.isFav = favOrBlock.IsFavorite;
                        }

                        result.Add(add);
                    }
                }

                return new JsonResult(result);
            }
            return new JsonResult("false");
        }

        /*==== favourite pro =====*/
        [HttpPost]
        public IActionResult SetFavouritePro(CustomerFavouritePros data)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                FavoriteAndBlocked set = _db.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == Id && x.TargetUserId == data.SpId);

                if(set == null)
                {
                    FavoriteAndBlocked add = new FavoriteAndBlocked();
                    add.UserId = (int)Id;
                    add.TargetUserId = data.SpId;
                    add.IsFavorite = true;
                    _db.FavoriteAndBlockeds.Add(add);
                }
                else
                {
                    if (set.IsFavorite == true)
                    {
                        set.IsFavorite = false;
                    }
                    else
                    {
                        set.IsFavorite = true;
                        set.IsBlocked = false;
                    }
                    _db.FavoriteAndBlockeds.Update(set);
                }

                _db.SaveChanges();

                return Ok(Json("true"));
                
            }
            return Ok(Json("false"));
        }

        /*==== block pro =====*/
        [HttpPost]
        public IActionResult SetBlockPro(CustomerFavouritePros data)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                FavoriteAndBlocked set = _db.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == Id && x.TargetUserId == data.SpId);

                if (set == null)
                {
                    FavoriteAndBlocked add = new FavoriteAndBlocked();
                    add.UserId = (int)Id;
                    add.TargetUserId = data.SpId;
                    add.IsBlocked = true;
                    _db.FavoriteAndBlockeds.Add(add);
                }
                else
                {
                    if (set.IsBlocked == true) {
                        set.IsBlocked = false;                    
                    }
                    else
                    {
                        set.IsFavorite = false;
                        set.IsBlocked = true;
                    }
                    _db.FavoriteAndBlockeds.Update(set);
                }

                _db.SaveChanges();

                return Ok(Json("true"));

            }
            return Ok(Json("false"));
        }

        /*===== get fav sp for book service =====*/
        [HttpGet]
        public JsonResult GetFavSPForService(CompleteBooking data)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id != null)
            {
                List<int> spIdList = _db.FavoriteAndBlockeds.Where(x => x.UserId == Id && x.IsFavorite == true).Select(x => x.TargetUserId).ToList();
                List<CustomerFavouritePros> result = new List<CustomerFavouritePros>();

                DateTime date =DateTime.Parse(data.ServiceStartDate.ToString("dd/MM/yyyy") + " " + data.ServiceTime);

                foreach (var spId in spIdList)
                {
                    var checkSPPostal = _db.Users.FirstOrDefault(x => x.UserId == spId && x.ZipCode == data.PostalCode);

                    if (checkSPPostal != null)
                    {

                        var obj = _db.ServiceRequests.FirstOrDefault(x =>
                     x.ServiceProviderId == spId && x.Status == 2 && ((x.ServiceStartDate <= date && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) >= date) || (x.ServiceStartDate <= date.AddHours((double)data.SubTotal + 1) && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) >= date.AddHours((double)data.SubTotal + 1)) ||
             (x.ServiceStartDate >= date.AddHours((double)data.SubTotal + 1) && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) <= date.AddHours((double)data.SubTotal + 1))
                     ));
                        if (obj == null)
                        {
                            CustomerFavouritePros add = new CustomerFavouritePros();
                            add.SpId = spId;
                            add.SpName = _db.Users.FirstOrDefault(x => x.UserId == spId).FirstName + " " + _db.Users.FirstOrDefault(x => x.UserId == spId).LastName;
                            result.Add(add);
                        }
                    }
                }
                return new JsonResult(result);
            }
            return new JsonResult("false");            
        }

        /*===== Service Schedule =======*/
        [HttpGet]
        public JsonResult GetServiceSchedule()
        {

            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                var id = HttpContext.Session.GetInt32("userId");
                
                List<CustomerDashbord> dashbord = new List<CustomerDashbord>();

                var table = _db.ServiceRequests.Where(x => x.UserId == id && x.Status != 1).ToList();

                foreach (var data in table)
                {

                    CustomerDashbord sr = new CustomerDashbord();
                    sr.ServiceRequestId = data.ServiceRequestId;

                    sr.ServiceStartDate = data.ServiceStartDate.ToString("yyyy-MM-dd");
                    sr.StartTime = data.ServiceStartDate.ToString("HH:mm");
                    sr.EndTime = data.ServiceStartDate.AddHours((double)data.SubTotal).ToString("HH:mm");

                    sr.Status = data.Status;

                    dashbord.Add(sr);
                }
                return new JsonResult(dashbord);
            }
            return new JsonResult("false");

        }

    }
}



