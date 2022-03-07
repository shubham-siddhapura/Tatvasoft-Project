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
    }
}
