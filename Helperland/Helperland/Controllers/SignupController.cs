using Helperland.Data;
using Helperland.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Helperland.Controllers
{
    public class SignupController : Controller
    {
        private readonly HelperlandContext _db;

        public SignupController(HelperlandContext db)
        {
            _db = db;
        }

        public IActionResult Signup()
        {
            int? Id = HttpContext.Session.GetInt32("userId");
            
            if(Id != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return PartialView();
        }

        [HttpPost]
        public IActionResult Signup(User user)
        {

            if (ModelState.IsValid)
            {
                if (_db.Users.Where(x => x.Email == user.Email).Count() == 0 && _db.Users.Where(x => x.Mobile == user.Mobile).Count() == 0)
                {
                    user.UserTypeId = 1;
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "User already exists";
                    return PartialView();
                }

            }
            return PartialView();
        }

        public IActionResult BecomeHelper()
        {
            return PartialView();
        }

   
        [HttpPost]
        public IActionResult BecomeHelper(User user)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.Where(x => x.Email == user.Email).Count() == 0 && _db.Users.Where(x => x.Mobile == user.Mobile).Count() == 0)
                {
                    user.UserTypeId = 2;
                    user.CreatedDate = DateTime.Now;
                    user.ModifiedDate = DateTime.Now;
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "User already exists";
                    return PartialView();
                }

            }
            return PartialView();

        }


        [HttpPost]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                User userLogin = _db.Users.FirstOrDefault(x => x.Email == loginUser.Email);
                if(userLogin != null)
                {

                    string password = userLogin.Password;
                
                    bool pass = BCrypt.Net.BCrypt.Verify(loginUser.Password, password);
                    if (_db.Users.Where(x => x.Email == loginUser.Email && pass).Count() > 0)
                    {
                        var user = _db.Users.FirstOrDefault(x => x.Email == loginUser.Email);

                        if (loginUser.Remember == true)
                        {
                            CookieOptions cookieRemember = new CookieOptions();
                            cookieRemember.Expires = DateTime.Now.AddMinutes(60);
                            Response.Cookies.Append("userId", Convert.ToString(user.UserId), cookieRemember);

                        }

                        HttpContext.Session.SetInt32("userId", user.UserId);

                        if (user.UserTypeId == 1)
                        {
                            return RedirectToAction("CustServiceHistory", "CustomerPage");
                        }
                        else if (user.UserTypeId == 2)
                        {
                            return RedirectToAction("SPUpcomingService", "ServicePro");
                        }
                        else if (user.UserTypeId == 3)
                        {
                            return RedirectToAction("ServiceRequest", "Admin");
                        }
                    }   
                }
               
                    TempData["showAlert"] = "show alert";
                    TempData["loginFail"] = "Username and Password are invalid.";
                    return RedirectToAction("Index", "Home", new { loginFail = "true" });
                
            }
            
            return PartialView();
        }

        
        [HttpPost]
        public IActionResult SendMail(string email)
        {

            if (email != null)
            {
                var user = _db.Users.FirstOrDefault(x => x.Email == email);

                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.UseDefaultCredentials = true;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("Id", "Pwd");

                MailMessage message = new MailMessage();
                
                
                    message.Subject = "Reset Password";
                    message.Body = "<h1>Reset your password by click below link</h1>" +
                    "<a href='" + Url.Action("ResetPassword", "Signup", new { userId = user.UserId, token = user.Password }, "https") + "'>Reset Password</a>";
          
                message.From = new MailAddress("siddshubham123456789@gmail.com");
                message.IsBodyHtml = true;

                message.To.Add(user.Email);
                client.Send(message);


                
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ResetPassword(int userId, string token)
        {
            TempData["id"] = userId;
            User user = _db.Users.FirstOrDefault(x => x.UserId == userId);
            if(token == user.Password)
            {
                return PartialView();
            }
            return RedirectToAction("Index", "Home");
        }
      
        [HttpPost]
        public IActionResult ResetPassword(ResetPassword rp)
        {
            String HashPwd = BCrypt.Net.BCrypt.HashPassword(rp.password);
            var user = new User() { UserId = rp.userId, Password = HashPwd, ModifiedDate = DateTime.Now };
            _db.Users.Attach(user);
            _db.Entry(user).Property(x => x.Password).IsModified = true;
            _db.Entry(user).Property(x => x.ModifiedDate).IsModified = true;
            _db.SaveChanges();


            return RedirectToAction("Index", "Home", new { loginModal = "true" });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            foreach(var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            
            
            return RedirectToAction("Index", "Home", new { logoutModal = "true"});
        }

        
    }



}
