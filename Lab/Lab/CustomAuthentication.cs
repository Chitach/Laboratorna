using Lab.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.Models;
using System.Security;
using System.Security.Principal;

namespace Lab
{
    public class CustomAuthentication
    {
        private const string cookieName = "__AUTH_COOKIE";

        public HttpContext HttpContext { get; set; }

        private readonly ApplicationDbContext _context;

        public CustomAuthentication(ApplicationDbContext context, HttpContext HttpContext)
        {
            _context = context;
            this.HttpContext = HttpContext;
        }

        #region IAuthentication Members

        public User Login(string userName, string Password)
        {
            User retUser = _context.Users.FirstOrDefault(x => x.Email == userName && x.Password == Password);
            if (retUser != null)
            {
                CreateCookie(userName);
            }
            return retUser;
        }

        public User GetSession()
        {
            var httpCookie = HttpContext.Request.Cookies[cookieName];
            if (httpCookie != null)
                return _context.Users.FirstOrDefault(p => string.Compare(p.UserName, httpCookie, true) == 0);
            else return null;
        }

        /*public User Login(string userName)
        {
            User retUser = _context.Users.FirstOrDefault(p => string.Compare(p.UserName, userName, true) == 0);
            if (retUser != null)
            {
                CreateCookie(userName);
            }
            return retUser;
        }*/

        private void CreateCookie(string userName)
        {
            HttpContext.Response.Cookies.Append(cookieName, userName, new CookieOptions() { Expires = new DateTimeOffset(DateTime.Now.AddMonths(1)) });
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Request.Cookies[cookieName];
            if (httpCookie != null)
            {
                HttpContext.Response.Cookies.Append(cookieName, "", new CookieOptions() { Expires = new DateTimeOffset(DateTime.Now) });
            }
        }
        #endregion
    }

}
