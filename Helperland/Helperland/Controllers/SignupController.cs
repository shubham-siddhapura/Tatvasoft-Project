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
                if (_db.Users.Where(x => x.Email == loginUser.Email && x.Password == loginUser.Password).Count() > 0)
                {
                    var user = _db.Users.FirstOrDefault(x => x.Email == loginUser.Email);
            
 
                    HttpContext.Session.SetInt32("userId", user.UserId);
                    return RedirectToAction("CustServiceHistory", "CustomerPage");
                }
                else
                {
                    TempData["showAlert"] = "show alert";
                    TempData["loginFail"] = "Username and Password are invalid.";
                    return RedirectToAction("Index", "Home", new { loginFail = "true" });
                }
            }
            

            return PartialView();
        }

        /*[HttpPost]*/
        /*public  IActionResult Forget(ForgetPassword email)
        {
            if (ModelState.IsValid)
            {
                var token = await userManager.GeneratePasswordResetTockenAsync(email);
            }
            *//*else
            {
                return View(Model);
            }*//*
            return RedirectToAction("Index", "Home");
        }*/

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
                    "<a href='" + Url.Action("ResetPassword", "Signup", new { userId = user.UserId }, "https") + "'>Reset Password</a>";
                

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("siddshubham123456789@gmail.com", "Shubham123");
                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
                return RedirectToAction("Index", "Home", new { mailSended="true" });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ResetPassword(int userId)
        {
            TempData["id"] = userId;
            return PartialView();
        }
      
        [HttpPost]
        public IActionResult ResetPassword(ResetPassword rp)
        {
            var user = new User() { UserId = rp.userId, Password = rp.password, ModifiedDate = DateTime.Now };
            _db.Users.Attach(user);
            _db.Entry(user).Property(x => x.Password).IsModified = true;
            _db.Entry(user).Property(x => x.Password).IsModified = true;
            _db.SaveChanges();


            return RedirectToAction("Index", "Home", new { loginModal = "true" });
        }

        /*public void Logout()
        {
            HttpContext.Session.Clear();
        }
*/
    }



}
