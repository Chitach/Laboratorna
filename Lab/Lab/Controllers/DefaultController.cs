using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab.Data;

namespace Lab.Controllers
{
    public class DefaultController : Controller
    {
        protected readonly ApplicationDbContext _context;
        protected readonly CustomAuthentication auth;

        public DefaultController(ApplicationDbContext context)
        {
            _context = context;
            auth = new CustomAuthentication(context, HttpContext);
            if (HttpContext != null)
                ViewData["user"] = auth.GetSession();
        }
    }
}