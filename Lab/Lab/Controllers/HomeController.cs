using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Lab.Data;

namespace Lab.Controllers
{
    public class HomeController : DefaultController
    {
        public HomeController(ApplicationDbContext context) : base(context)
        {
            if (!context.Doctors.Any())
            {
                context.Doctors.Add(new Models.Doctor()
                {
                    Email = "admin@admin.com",
                    IsMale = true,
                    Password = "admin",
                    UserName = "admin",
                    FirstName = "admin",
                    Lastname = "admin",
                    BirthDate = DateTime.Now,
                    Address = "",
                });
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
