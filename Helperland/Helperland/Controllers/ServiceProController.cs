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
        
        [HttpPost]
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
                
                User user = _db.Users.Find(id);
                TempData["Name"] = user.FirstName;
                TempData["userType"] = user.UserTypeId.ToString();
                if (user.UserTypeId == 2)
                {

                    List<ServiceRequest> requests = _db.ServiceRequests.Where(x => x.ZipCode == user.ZipCode && x.Status == 2 && x.ServiceProviderId == null).ToList();


                    List<NewServiceRequestTable> table = new List<NewServiceRequestTable>();

                    foreach(ServiceRequest row in requests)
                    {
                        NewServiceRequestTable tablerow = new NewServiceRequestTable();

                        tablerow.ServiceRequestId = row.ServiceRequestId;
                        tablerow.ServiceStartDate = row.ServiceStartDate.ToString("dd/MM/yyyy");
                        tablerow.StartTime = row.ServiceStartDate.ToString("HH:mm");
                        tablerow.EndTime = row.ServiceStartDate.AddHours((double)row.SubTotal).ToString("HH:mm tt");
                        tablerow.TotalCost = row.TotalCost;
                        tablerow.HasPet = row.HasPets;

                        var obj = _db.ServiceRequests.FirstOrDefault(x => 
                        (x.ServiceProviderId == id && x.ServiceStartDate <= row.ServiceStartDate && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) >= row.ServiceStartDate) ||  (x.ServiceProviderId == id && x.ServiceStartDate <= row.ServiceStartDate.AddHours((double)row.SubTotal+1) && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) >= row.ServiceStartDate.AddHours((double)row.SubTotal + 1)) ||
                        (x.ServiceProviderId == id && x.ServiceStartDate >= row.ServiceStartDate.AddHours((double)row.SubTotal + 1) && x.ServiceStartDate.AddHours((double)x.SubTotal + 1) <= row.ServiceStartDate.AddHours((double)row.SubTotal + 1))
                        );
                        if(obj!= null)
                        {
                            int conflict = obj.ServiceRequestId;
                            
                            tablerow.TimeConflict = conflict;
                        }


                        ServiceRequestAddress address = _db.ServiceRequestAddresses.FirstOrDefault(x => x.ServiceRequestId == row.ServiceRequestId);


                        tablerow.AddressLine1 = address.AddressLine1;
                        tablerow.AddressLine2 = address.AddressLine2;
                        tablerow.City = address.City;
                        tablerow.PostalCode = address.PostalCode;

                        User customer = _db.Users.FirstOrDefault(x => x.UserId == row.UserId);

                        tablerow.CustomerName = customer.FirstName + " " + customer.LastName;


                        table.Add(tablerow);
                    }

                    return PartialView(table);
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

                
                if(_db.UserAddresses.Where(x=>x.UserId == (int)Id).Count() > 0)
                {
                    UserAddress address = _db.UserAddresses.FirstOrDefault(x => x.UserId == (int)Id);

                    string oldZipcode = address.PostalCode;


                    address.AddressLine1 = data.UserAddresses.First().AddressLine1;
                    address.AddressLine2 = data.UserAddresses.First().AddressLine2;
                    address.City = data.UserAddresses.First().City;
                    address.PostalCode = data.UserAddresses.First().PostalCode;
                    address.State = data.UserAddresses.First().State;
                    user.ZipCode = data.UserAddresses.First().PostalCode;

                    _db.UserAddresses.Update(address);
                    _db.SaveChanges();

                    if(oldZipcode != address.PostalCode)
                    {
                        int otherUsesZip = _db.UserAddresses.Where(x => x.PostalCode == oldZipcode).Count();

                        if (otherUsesZip==0)
                        {
                            Zipcode deleteZip = _db.Zipcodes.FirstOrDefault(x=>x.ZipcodeValue == oldZipcode);
                            _db.Zipcodes.Remove(deleteZip);
                            _db.SaveChanges();
                        }

                        Zipcode zipcodes = _db.Zipcodes.FirstOrDefault(x => x.ZipcodeValue == user.ZipCode);
                        if (zipcodes == null)
                        {
                            State state = _db.States.FirstOrDefault(x => x.StateName == data.UserAddresses.First().State);

                            if (state == null)
                            {

                                state.StateName = data.UserAddresses.First().State;
                                var saveState = _db.States.Add(state);
                                _db.SaveChanges();

                                City city = new City();
                                city.CityName = data.UserAddresses.First().City;
                                city.StateId = saveState.Entity.Id;
                                var saveCity = _db.Cities.Add(city);
                                _db.SaveChanges();

                                Zipcode zip = new Zipcode();
                                zip.ZipcodeValue = data.UserAddresses.First().PostalCode;
                                zip.CityId = saveCity.Entity.Id;
                                _db.Zipcodes.Add(zip);
                                _db.SaveChanges();
                            }
                            else
                            {
                                City city = _db.Cities.FirstOrDefault(x => x.CityName == data.UserAddresses.First().City);
                                if (city == null)
                                {
                                    city.CityName = data.UserAddresses.First().City;
                                    city.StateId = state.Id;

                                    var saveCity = _db.Cities.Add(city);
                                    _db.SaveChanges();

                                    Zipcode zip = new Zipcode();
                                    zip.ZipcodeValue = data.UserAddresses.First().PostalCode;
                                    zip.CityId = saveCity.Entity.Id;
                                    _db.Zipcodes.Add(zip);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    Zipcode zip = new Zipcode();
                                    zip.ZipcodeValue = data.UserAddresses.First().PostalCode;
                                    zip.CityId = city.Id;
                                    _db.Zipcodes.Add(zip);
                                    _db.SaveChanges();
                                }
                            }

                        }


                    }

                    
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

                    Zipcode zipcodes = _db.Zipcodes.FirstOrDefault(x => x.ZipcodeValue == user.ZipCode);
                    if (zipcodes == null)
                    {
                        State state = _db.States.FirstOrDefault(x => x.StateName == data.UserAddresses.First().State);

                        if (state == null)
                        {

                            state.StateName = data.UserAddresses.First().State;
                            var saveState = _db.States.Add(state);
                            _db.SaveChanges();

                            City city = new City();
                            city.CityName = data.UserAddresses.First().City;
                            city.StateId = saveState.Entity.Id;
                            var saveCity = _db.Cities.Add(city);
                            _db.SaveChanges();

                            Zipcode zip = new Zipcode();
                            zip.ZipcodeValue = data.UserAddresses.First().PostalCode;
                            zip.CityId = saveCity.Entity.Id;
                            _db.Zipcodes.Add(zip);
                            _db.SaveChanges();
                        }
                        else
                        {
                            City city = _db.Cities.FirstOrDefault(x => x.CityName == data.UserAddresses.First().City);
                            if (city == null)
                            {
                                city.CityName = data.UserAddresses.First().City;
                                city.StateId = state.Id;

                                var saveCity = _db.Cities.Add(city);
                                _db.SaveChanges();

                                Zipcode zip = new Zipcode();
                                zip.ZipcodeValue = data.UserAddresses.First().PostalCode;
                                zip.CityId = saveCity.Entity.Id;
                                _db.Zipcodes.Add(zip);
                                _db.SaveChanges();
                            }
                            else
                            {
                                Zipcode zip = new Zipcode();
                                zip.ZipcodeValue = data.UserAddresses.First().PostalCode;
                                zip.CityId = city.Id;
                                _db.Zipcodes.Add(zip);
                                _db.SaveChanges();
                            }
                        }

                    }


                    _db.UserAddresses.Add(address);
                    _db.SaveChanges();
                }

                _db.Users.Update(user);
                _db.SaveChanges();

                return Ok(Json("true"));
 
            }

            return Ok(Json("false"));
        }

        [HttpPost]
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

        [HttpGet]
        public IActionResult GetNewServiceRequests()
        {

            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id == null && Request.Cookies["userid"] != null)
            {
                HttpContext.Session.SetInt32("userId", Convert.ToInt32(Request.Cookies["userId"]));
                Id = HttpContext.Session.GetInt32("userId");
            }



            return Ok(Json("false"));
        } 


        /* =============== Upcoming services ========================== */

        [HttpGet]
        public JsonResult GetUpcomingHistory()
        {

            int? Id = HttpContext.Session.GetInt32("userId");
            
            if (Id != null)
            {
                List<ServiceRequest> requests = _db.ServiceRequests.Where(x=>x.Status == 2 && x.ServiceProviderId == (int)Id).ToList();


                List<NewServiceRequestTable> table = new List<NewServiceRequestTable>();

                foreach (ServiceRequest row in requests)
                {
                    NewServiceRequestTable tablerow = new NewServiceRequestTable();

                    tablerow.ServiceRequestId = row.ServiceRequestId;
                    tablerow.ServiceStartDate = row.ServiceStartDate.ToString("dd/MM/yyyy");
                    tablerow.StartTime = row.ServiceStartDate.ToString("HH:mm");
                    tablerow.EndTime = row.ServiceStartDate.AddHours((double)row.SubTotal).ToString("HH:mm tt");

                    tablerow.TotalCost = row.TotalCost;

                    ServiceRequestAddress address = _db.ServiceRequestAddresses.FirstOrDefault(x => x.ServiceRequestId == row.ServiceRequestId);

                    tablerow.AddressLine1 = address.AddressLine1;
                    tablerow.AddressLine2 = address.AddressLine2;
                    tablerow.City = address.City;
                    tablerow.PostalCode = address.PostalCode;

                    User customer = _db.Users.FirstOrDefault(x => x.UserId == row.UserId);

                    tablerow.CustomerName = customer.FirstName + " " + customer.LastName;

                    DateTime serviceDate = row.ServiceStartDate.AddHours((double)row.SubTotal);

                    DateTime currentTime = DateTime.Now;
                    if(serviceDate<= currentTime)
                    {
                        tablerow.Completed = true;
                    }
                    else
                    {
                        tablerow.Completed = false;
                    }
                     

                    table.Add(tablerow);
                }

                
                return new JsonResult(table);
                
            }
            
            return new JsonResult("false");
        }


        /*================ Rating =======================*/
        [HttpGet]
        public JsonResult GetMyRatings()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                List<Rating> rating = _db.Ratings.Where(x=>x.RatingTo == Id).ToList();
                
                foreach(Rating row in rating)
                {
                    User user = _db.Users.FirstOrDefault(x => x.UserId == row.RatingTo);
                    row.RatingToNavigation = user;

                    ServiceRequest sr = _db.ServiceRequests.FirstOrDefault(x=>x.ServiceRequestId == row.ServiceRequestId);

                    row.ServiceRequest = sr;
                }

                if(rating != null)
                {
                    return new JsonResult(rating);
                }                

            }
            return new JsonResult(false);
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
            Details.HasPet = sr.HasPets;
            Details.ZipCode = sr.ZipCode; 

            List<ServiceRequestExtra> Extra = _db.ServiceRequestExtras.Where(x => x.ServiceRequestId == ID.ServiceRequestId).ToList();

            foreach (ServiceRequestExtra row in Extra)
            {
                if (row.ServiceExtraId == 1)
                {
                    Details.Cabinet = true;
                }
                else if (row.ServiceExtraId == 2)
                {
                    Details.Oven = true;
                }
                else if (row.ServiceExtraId == 3)
                {
                    Details.Window = true;
                }
                else if (row.ServiceExtraId == 4)
                {
                    Details.Fridge = true;
                }
                else
                {
                    Details.Laundry = true;
                }
            }

            ServiceRequestAddress Address = _db.ServiceRequestAddresses.FirstOrDefault(x => x.ServiceRequestId == ID.ServiceRequestId);

            Details.Address = Address.AddressLine1 + ", " + Address.AddressLine2 + ", " + Address.City + " - " + Address.PostalCode;

            Details.PhoneNo = Address.Mobile;
            Details.Email = Address.Email;

            User user = _db.Users.FirstOrDefault(x => x.UserId == sr.UserId);
            Details.CustomerName = user.FirstName + " " + user.LastName;

            return new JsonResult(Details);
        }

        [HttpPost]
        public IActionResult AcceptServiceRequest(ServiceRequest sr)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {
                ServiceRequest updateSr = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == sr.ServiceRequestId);
                if(updateSr.ServiceProviderId == null) {

                    updateSr.ServiceProviderId = (int)Id;
                    updateSr.SpacceptedDate = DateTime.Now;
                    _db.ServiceRequests.Update(updateSr);
                    _db.SaveChanges();

                    return Ok(Json("true"));

                }
                else
                {
                    return Ok(Json("already"));
                }
                
            }
            return Ok(Json("false"));
        }

        /*============ Cancel Request ==================*/
        [HttpPost]
        public IActionResult CancelServiceRequest(ServiceRequest cancel)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {

                ServiceRequest cancelService = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == cancel.ServiceRequestId);
                DateTime current = DateTime.Now;

                cancelService.ServiceProviderId = null;
                cancelService.SpacceptedDate = null;
                cancelService.ModifiedDate = DateTime.Now;
                cancelService.ModifiedBy = Id;
                if (cancelService.ServiceStartDate.AddHours((double)cancelService.SubTotal)<= current)
                {
                    // status 1 is for cancel requests
                    cancelService.Status = 1;
                    
                }

                var result = _db.ServiceRequests.Update(cancelService);
                _db.SaveChanges();
                if (result != null)
                {
                    return Ok(Json("true"));
                }
            }
            return Ok(Json("false"));

        }

        [HttpPost]
        public IActionResult CompleteServiceRequest(ServiceRequest complete)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null)
            {

                ServiceRequest completeService = _db.ServiceRequests.FirstOrDefault(x => x.ServiceRequestId == complete.ServiceRequestId);
                DateTime current = DateTime.Now;

                if (completeService.ServiceStartDate.AddHours((double)completeService.SubTotal) <= current)
                {
                    // status 0 is for complete requests
                    completeService.Status = 0;

                    completeService.ModifiedDate = DateTime.Now;
                    completeService.ModifiedBy = Id;

                }
                else
                {
                    return Ok(Json("notPossible"));
                }

                var result = _db.ServiceRequests.Update(completeService);
                _db.SaveChanges();
                if (result != null)
                {
                    return Ok(Json("true"));
                }
            }
            return Ok(Json("false"));

        }

        [HttpGet]
        public JsonResult GetUsersForBlock()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if(Id!= null)
            {
                List<int> users = _db.ServiceRequests.Where(x => x.ServiceProviderId == (int)Id && x.Status == 0).Select(x=>x.UserId).Distinct().ToList();

                List<FavoriteAndBlocked> result = new List<FavoriteAndBlocked>();
                foreach(int userId in users)
                {
                    FavoriteAndBlocked fav = _db.FavoriteAndBlockeds.Where(x=>x.UserId == (int)Id && x.TargetUserId == userId).FirstOrDefault();

                    if(fav == null)
                    {
                        fav = new FavoriteAndBlocked();
                    }
                    fav.TargetUser = _db.Users.Where(x => x.UserId == userId).FirstOrDefault();

                    result.Add(fav);

                }

                return new JsonResult(result);
            }

            return new JsonResult("false");
        }

        [HttpPost]
        public IActionResult BlockOrUnblockCustomer(FavoriteAndBlocked user)
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            if (Id != null) {
                FavoriteAndBlocked fav = _db.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == (int)Id && x.TargetUserId == user.TargetUserId);
                if (fav == null)
                {
                    FavoriteAndBlocked add = new FavoriteAndBlocked();
                    add.UserId = (int)Id;
                    add.TargetUserId = user.TargetUserId;
                    add.IsBlocked = true;

                    _db.FavoriteAndBlockeds.Add(add);
                    _db.SaveChanges();
                }
                else {

                    if (fav.IsBlocked == true)
                    {
                        fav.IsBlocked = false;
                    }
                    else
                    {
                        fav.IsBlocked = true;
                    }
                    _db.FavoriteAndBlockeds.Update(fav);
                    _db.SaveChanges();
                }
                return Ok(Json("true"));
            }
            return Ok(Json("false"));
        }
    }
}
