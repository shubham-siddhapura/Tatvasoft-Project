using Helperland.Data;
using Helperland.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Org.BouncyCastle.Crypto.Generators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Memory;

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

                MimeMessage message = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Helperland",
                "siddshubham123456789@gmail.com");
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress(user.FirstName, email);
                message.To.Add(to);

                message.Subject = "Reset Password";

                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<h1>Reset your password by click below link</h1>" +
                    "<a href='" + Url.Action("ResetPassword", "Signup", new { userId = user.UserId, token=user.Password }, "https") + "'>Reset Password</a>";
                
                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("id", "pwd");
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                TempData["mailSended"] = "true";
                return RedirectToAction("Index", "Home", new { mailSended="true" });

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
