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
    public class AdminController : Controller
    {
        private readonly HelperlandContext _db;

        public AdminController(HelperlandContext db)
        {
            _db = db;
        }

        public IActionResult ServiceRequest()
        {
            
            int? id = HttpContext.Session.GetInt32("userId");
            if (id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                id = HttpContext.Session.GetInt32("userId");
            }
            if (id != null)
            {
                User user = _db.Users.Find(id);
                if(user.UserTypeId == 3)
                {
                    TempData["Name"] = user.FirstName;
                    return PartialView();
                }
            }
            
            return RedirectToAction("Index", "Home", new { loginModal = "true" });
        }

        [HttpPost]
        public IActionResult GetServiceRequests(AdminServiceRequests myData)
        {

            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var serviceRequests = (from sr in _db.ServiceRequests select sr);

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    if (sortColumnDirection == "asc")
                    {
                        switch (sortColumn)
                        {
                            case "ServiceId":
                                serviceRequests = from x in serviceRequests orderby x.ServiceRequestId ascending select x;
                                break;

                            case "Customer":
                                serviceRequests = from x in serviceRequests orderby x.User.FirstName ascending select x;
                                break;

                            case "ServiceProvider":
                                serviceRequests = from x in serviceRequests orderby x.ServiceProvider.FirstName ascending select x;
                                break;

                            case "Date":
                                serviceRequests = from x in serviceRequests orderby x.ServiceStartDate ascending select x;
                                break;

                            case "Status":
                                serviceRequests = serviceRequests.OrderBy(x => x.Status).ThenBy(x => x.ServiceProviderId);
                                break;


                            default:
                                serviceRequests = from x in serviceRequests orderby x.UserId ascending select x;
                                break;
                        }
                    }
                    else if (sortColumnDirection == "desc")
                    {
                        switch (sortColumn)
                        {
                            case "ServiceId":
                                serviceRequests = from x in serviceRequests orderby x.ServiceRequestId descending select x;
                                break;

                            case "Customer":
                                serviceRequests = from x in serviceRequests orderby x.User.FirstName descending select x;
                                break;

                            case "ServiceProvider":
                                serviceRequests = from x in serviceRequests orderby x.ServiceProvider.FirstName descending select x;
                                break;

                            case "Date":
                                serviceRequests = from x in serviceRequests orderby x.ServiceStartDate descending select x;
                                break;

                            case "Status":
                                serviceRequests = serviceRequests.OrderByDescending(x => x.Status).ThenByDescending(x => x.ServiceProviderId);
                                break;


                            default:
                                serviceRequests = from x in serviceRequests orderby x.UserId descending select x;
                                break;

                        }
                    }
                }

                if(myData.ServiceId != null)
                {
                    serviceRequests = serviceRequests.Where(x => x.ServiceRequestId == myData.ServiceId);
                }

                if (myData.CustomerName != null)
                {
                    serviceRequests = serviceRequests.Where(x => x.User.FirstName.Contains(myData.CustomerName) || x.User.LastName.Contains(myData.CustomerName));
                }

                if(myData.SPName != null)
                {
                    serviceRequests = serviceRequests.Where(x => x.ServiceProvider.FirstName.Contains(myData.SPName) || x.ServiceProvider.LastName.Contains(myData.SPName));
                }

                if (myData.Status != null)
                {
                    if(myData.Status == 2)
                    {
                        serviceRequests = serviceRequests.Where(x => x.Status == 2 && x.ServiceProviderId != null);
                    }
                    else if (myData.Status == 3)
                    {
                        serviceRequests = serviceRequests.Where(x => x.Status == 2 && x.ServiceProviderId == null);
                    }
                    else if (myData.Status != null)
                    {
                        serviceRequests = serviceRequests.Where(x => x.Status == myData.Status);
                    }
                    
                }

                if (myData.FromDate != null)
                {
                    serviceRequests = serviceRequests.Where(x => x.ServiceStartDate >= myData.FromDate);
                }

                if (myData.ToDate != null)
                {
                    serviceRequests = serviceRequests.Where(x => x.ServiceStartDate <= myData.ToDate);
                }

                recordsTotal = serviceRequests.Count();
                var Tempdata = serviceRequests.Skip(skip).Take(pageSize).ToList();
                List<AdminServiceRequests> data = new List<AdminServiceRequests>();
                for (int i = 0; i < Tempdata.Count(); i++)
                {
                    AdminServiceRequests service = new AdminServiceRequests();
                    UserAddress uaddr = _db.UserAddresses.FirstOrDefault(x => x.UserId == Tempdata[i].UserId);

                    service.ServiceId = Tempdata[i].ServiceRequestId;

                    service.ServiceStartDate = Tempdata[i].ServiceStartDate.ToString("dd/MM/yyyy");

                    service.StartTime = Tempdata[i].ServiceStartDate.ToString("HH:mm");

                    service.EndTime = Tempdata[i].ServiceStartDate.AddHours((double)Tempdata[i].SubTotal).ToString("HH:mm");

                    User Customer = _db.Users.FirstOrDefault(x=>x.UserId == Tempdata[i].UserId);

                    service.CustomerName = Customer.FirstName + " " + Customer.LastName;

                    ServiceRequestAddress srAddr = _db.ServiceRequestAddresses.FirstOrDefault(x => x.ServiceRequestId == Tempdata[i].ServiceRequestId);

                    service.CustomerAddress = srAddr.AddressLine2 + ", " + srAddr.AddressLine1 + ", " + srAddr.City + " - " + srAddr.PostalCode;

                    if (Tempdata[i].ServiceProviderId != null)
                    {
                        User ServiceProvider = _db.Users.FirstOrDefault(x => x.UserId == Tempdata[i].ServiceProviderId);

                        service.SPName = ServiceProvider.FirstName + " " + ServiceProvider.LastName;

                        var sRating = _db.Ratings.Where(x => x.RatingTo == Tempdata[i].ServiceProviderId);

                        if (sRating.Count() > 0)
                        {
                            service.Rating = sRating.Average(x => x.Ratings);
                        }

                    }
                    service.Status = Tempdata[i].Status;

                    data.Add(service);
                }
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            return Ok(Json("false"));
        }

        [HttpPost]
        public IActionResult GetUsers(AdminUserMng myData)
        {

            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var customerData = (from users in _db.Users select users);

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                  {
                    if (sortColumnDirection == "asc")
                    {
                        switch (sortColumn) {
                            case "UserName":
                                customerData = from x in customerData orderby x.FirstName ascending select x;
                                break;

                            case "UserTypeId":
                                customerData = from x in customerData orderby x.UserTypeId ascending select x;
                                break;

                            case "ZipCode":
                                customerData = from x in customerData orderby x.ZipCode ascending select x;
                                break;

                            case "Mobile":
                                customerData = from x in customerData orderby x.Mobile ascending select x;
                                break;

                            case "Date":
                                customerData = from x in customerData orderby x.CreatedDate ascending select x;
                                break;

                            case "Status":
                                customerData = from x in customerData orderby x.Status ascending select x;
                            break;


                            default:
                                customerData = from x in customerData orderby x.UserId ascending select x;
                                break;
                        }
                    }
                    else if (sortColumnDirection == "desc")
                    {
                        switch (sortColumn) {
                            case "UserName":
                                customerData = from x in customerData orderby x.FirstName descending select x;
                                break;

                            case "UserTypeId":
                                customerData = from x in customerData orderby x.UserTypeId descending select x;
                                break;

                            case "ZipCode":
                                customerData = from x in customerData orderby x.ZipCode descending select x;
                                break;

                            case "Mobile":
                                customerData = from x in customerData orderby x.Mobile descending select x;
                                break;


                            default:
                                    customerData = from x in customerData orderby x.UserId descending select x;
                                break;
                        }
                    }
                }

                
                if (myData.UserName != null)
                {
                    customerData = customerData.Where(x => x.FirstName.Contains(myData.UserName) || x.LastName.Contains(myData.UserName));
                }

                if(myData.UserType != null)
                {
                    if (myData.UserType == "customer")
                    {
                        customerData= customerData.Where(x => x.UserTypeId==1);
                    }
                    else if (myData.UserType == "sp")
                    {
                        customerData = customerData.Where(x => x.UserTypeId == 2);
                    }
                    else if (myData.UserType == "admin")
                    {
                        customerData = customerData.Where(x => x.UserTypeId == 3);
                    }
                }

                if (myData.PhoneNo!=null)
                {
                    customerData = customerData.Where(x => x.Mobile.Contains(myData.PhoneNo));
                }

                if(myData.ZipCode != null)
                {
                    customerData = customerData.Where(x => x.ZipCode.Contains(myData.ZipCode));
                }

                recordsTotal = customerData.Count();
                var Tempdata = customerData.Skip(skip).Take(pageSize).ToList();
                List<AdminUserMng> data = new List<AdminUserMng>();
                for (int i = 0; i < Tempdata.Count(); i++)
                {
                    AdminUserMng userMng = new AdminUserMng();
                    UserAddress uaddr = _db.UserAddresses.FirstOrDefault(x => x.UserId == Tempdata[i].UserId);

                    userMng.UserId = Tempdata[i].UserId;
                    userMng.UserName = Tempdata[i].FirstName + " " + Tempdata[i].LastName;
                    userMng.ZipCode = Tempdata[i].ZipCode;
                    userMng.PhoneNo = Tempdata[i].Mobile;
                    userMng.RegDate = Tempdata[i].CreatedDate.ToString("dd/MM/yyyy");
                    if (Tempdata[i].UserTypeId == 1)
                    {
                        userMng.UserType = "Customer";
                    }
                    else if (Tempdata[i].UserTypeId == 2)
                    {
                        userMng.UserType = "Service Provider";
                    }
                    else if (Tempdata[i].UserTypeId == 3)
                    {
                        userMng.UserType = "Admin";
                    }
                    userMng.Status = Tempdata[i].IsActive;

                    if (uaddr != null)
                        userMng.City = uaddr.City;

                    data.Add(userMng);
                }
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            return Ok(Json("false"));
        }


        /*============== Change user Status ================*/
        [HttpPost]
        public IActionResult ChangeUserStatus(User data)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                User user = _db.Users.FirstOrDefault(x => x.UserId == data.UserId);
                if(user != null)
                {
                    if(user.IsActive == true)
                    {
                        user.IsActive = false;
                    }
                    else
                    {
                        user.IsActive = true;
                    }

                    _db.Users.Update(user);
                    _db.SaveChanges();
                    return Ok(Json("true"));
                }
            }
            return Ok(Json("false"));
        }

        /*======Get Service Details for edit========*/
        public JsonResult GetServiceDetails(ServiceRequest data)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                ServiceRequest sr = _db.ServiceRequests.FirstOrDefault(x=>x.ServiceRequestId == data.ServiceRequestId);

                ServiceRequestAddress addr = _db.ServiceRequestAddresses.FirstOrDefault(x => x.ServiceRequestId == data.ServiceRequestId);

                sr.ServiceRequestAddresses.Add(addr);

                return new JsonResult(sr);
            }
            return new JsonResult("false");
        }

        [HttpPost]
        public IActionResult EditServiceRequest(ServiceRequest data)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id!= null)
            {
                ServiceRequest sr = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == data.ServiceRequestId);

                if(sr != null && sr.Status == 2)
                {
                    if(sr.ServiceProviderId != null)
                    {
                        data.SubTotal = sr.SubTotal;
                        int id =(int)sr.ServiceProviderId;
                        var obj = _db.ServiceRequests.FirstOrDefault(x => x.ServiceProviderId == id && x.Status==2 && ((x.ServiceStartDate <= data.ServiceStartDate && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) >= data.ServiceStartDate) || (x.ServiceStartDate <=data.ServiceStartDate.AddHours((double)data.SubTotal + 1) && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) >= data.ServiceStartDate.AddHours((double)data.SubTotal + 1)) ||
                (x.ServiceStartDate >=data.ServiceStartDate.AddHours((double)data.SubTotal + 1) && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) <= data.ServiceStartDate.AddHours((double)data.SubTotal + 1)))
                        );

                        if (obj != null)
                        {
                            return Ok(Json("conflict"));
                        }
                    }

                    sr.ServiceStartDate = data.ServiceStartDate;

                    ServiceRequestAddress srAddr = _db.ServiceRequestAddresses.FirstOrDefault(x=>x.ServiceRequestId == sr.ServiceRequestId);

                    srAddr.AddressLine1 = data.ServiceRequestAddresses.First().AddressLine1;

                    srAddr.AddressLine2 = data.ServiceRequestAddresses.First().AddressLine2;

                    srAddr.PostalCode = data.ServiceRequestAddresses.First().PostalCode;

                    srAddr.City = data.ServiceRequestAddresses.First().City;

                    _db.ServiceRequestAddresses.Update(srAddr);

                    _db.ServiceRequests.Update(sr);

                    _db.SaveChanges();

                    return Ok("true");

                }

                
            }
            return Ok(Json("false"));
        }
    }
}
