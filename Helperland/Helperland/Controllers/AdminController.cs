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

                /*  if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                  {
                      customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);
                  }*/

                
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
    }
}
