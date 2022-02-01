﻿using Helperland.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Helperland.Data;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Helperland.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HelperlandContext _db;

        private readonly IWebHostEnvironment _webHostEnv;

        /*   public HomeController(ILogger<HomeController> logger)
           {
               _logger = logger;
           }
   */
        public HomeController(HelperlandContext db, IWebHostEnvironment webHostEnv)
        {
            _db = db;
            _webHostEnv = webHostEnv;
        }

        public IActionResult Index()
        {
            return PartialView();
        }

        public IActionResult About()
        {
            return PartialView();
        }

        public IActionResult Contact()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult Contact(ContactU contactu)
        {
            if(contactu.Attach != null)
            {
                string folder = "contactFiles/";
                folder += Guid.NewGuid().ToString() + "_" + contactu.Attach.FileName;
                string serverFolder=Path.Combine(_webHostEnv.WebRootPath, folder);
                contactu.Attach.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                contactu.FileName = folder;
            }
            contactu.CreatedOn = DateTime.Now;
            _db.ContactUs.Add(contactu);
            _db.SaveChanges();
            return PartialView();
        }

        public IActionResult Prices()
        {
            return PartialView();
        }

        public IActionResult Faq()
        {
            return PartialView();
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
