using Helperland.Data;
using Helperland.Models;
using Helperland.ViewModels;
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
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            }
            if (id != null)
            {
                
                Models.User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                if (user.UserTypeId == 2)
                {
                    return PartialView();
                }

            }
            
            
            return RedirectToAction("Index", "Home", new { loginModal = "true" });
            
        }


        [HttpGet]
        public JsonResult GetServiceHistory()
        {

            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                Id = HttpContext.Session.GetInt32("userId");
            }
            if(Id != null)
            {
                List<SPNewServiceRequest> result = new List<SPNewServiceRequest>();
                List<ServiceRequest> table = _db.ServiceRequests.Where(x => x.ServiceProviderId == Id && x.Status == 0).ToList();

                foreach (ServiceRequest row in table)
                {
                    SPNewServiceRequest add = new SPNewServiceRequest();

                    add.ServiceId = row.ServiceRequestId;
                    add.ServiceStartDate = row.ServiceStartDate.ToString("dd/MM/yyyy");
                    add.ServiceStartTime = row.ServiceStartDate.ToString("HH:mm");
                    add.ServiceEndTime = row.ServiceStartDate.AddHours((double)row.SubTotal).ToString("HH:mm");


                    ServiceRequestAddress address = _db.ServiceRequestAddresses.FirstOrDefault(x => x.ServiceRequestId == row.ServiceRequestId);

                    add.CustomerAddress = address.AddressLine1 + " " + address.AddressLine2 + " " + address.City + " - " + address.PostalCode;

                    User user = _db.Users.FirstOrDefault(x => x.UserId == row.UserId);

                    add.CustomerName = user.FirstName + " " + user.LastName;

                    result.Add(add);
                }

                return new JsonResult(result);
            }
            return new JsonResult(false);
        }

        [HttpGet]
        public JsonResult GetSPDetails()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                Id = HttpContext.Session.GetInt32("userId");
            }
            if (Id != null)
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == Id);
                User data = new User();
                if (user != null)
                {
                    data = user;
                }

                UserAddress address = _db.UserAddresses.FirstOrDefault(x => x.UserId == Id);
                if(address != null)
                {
                    data.UserAddresses.Add(address);
                }

                return new JsonResult(data);
            }

            return new JsonResult(false);
        }

        [HttpPost]
        public IActionResult UpdateSPDetails(User data)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                Id = HttpContext.Session.GetInt32("userId");
            }
            if(Id != null)
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == Id);
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                if(_db.Users.Where(x=>x.Mobile == data.Mobile && x.UserId != user.UserId).Count() > 0)
                {
                    return Ok(Json("mobile"));
                }
                user.Mobile = data.Mobile;
                user.DateOfBirth = data.DateOfBirth;
                /*
                 gender:
                    1:male,
                    2:female,
                    3:not to say
                 */
                user.Gender = data.Gender;
                user.UserProfilePicture = data.UserProfilePicture;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = (int)Id;

                _db.Users.Update(user);
                _db.SaveChanges();

                if(_db.UserAddresses.Where(x=>x.UserId == (int)Id).Count() > 0)
                {
                    UserAddress address = _db.UserAddresses.FirstOrDefault(x => x.UserId == (int)Id);

                    address.AddressLine1 = data.UserAddresses.First().AddressLine1;
                    address.AddressLine2 = data.UserAddresses.First().AddressLine2;
                    address.City = data.UserAddresses.First().City;
                    address.PostalCode = data.UserAddresses.First().PostalCode;
                    address.State = data.UserAddresses.First().State;

                    _db.UserAddresses.Update(address);
                    _db.SaveChanges();
                }
                else
                {
                    UserAddress address = new UserAddress();

                    address.AddressLine1 = data.UserAddresses.First().AddressLine1;
                    address.AddressLine2 = data.UserAddresses.First().AddressLine2;
                    address.City = data.UserAddresses.First().City;
                    address.PostalCode = data.UserAddresses.First().PostalCode;
                    address.State = data.UserAddresses.First().State;

                    address.Mobile = data.Mobile;
                    address.UserId = data.UserId;
                    address.IsDefault = true;
                    address.IsDeleted = false;
                    
                    _db.UserAddresses.Add(address);
                    _db.SaveChanges();
                }
                return Ok(Json("true"));
 
            }

            return Ok(Json("false"));
        }

        public IActionResult ChangePassword(ChangePassword pwd)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == Id);
                if (BCrypt.Net.BCrypt.Verify(pwd.oldPassword, user.Password))
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

    }
}
