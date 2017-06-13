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

        public DefaultController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}