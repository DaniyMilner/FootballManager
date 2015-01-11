using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace manager.Components
{
    public interface IAuthenticationProvider
    {
        void SignIn(string username, bool isPersistent);
        void SignOut();
        bool IsUserAuthenticated();
    }

    public class AuthenticationProvider : IAuthenticationProvider
    {
        public void SignIn(string username, bool rememberMe = false)
        {
            //var expirationDate = rememberMe ? DateTime.Now.AddYears(2) : DateTime.Now.AddHours(3);
            var expirationDate = DateTime.Now.AddYears(2);
            var ticket =
                new FormsAuthenticationTicket(2, username, DateTime.Now, expirationDate, true,
                username, FormsAuthentication.FormsCookiePath);

            var ticketHash = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketHash) { Expires = expirationDate };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool IsUserAuthenticated()
        {
            return HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }
}