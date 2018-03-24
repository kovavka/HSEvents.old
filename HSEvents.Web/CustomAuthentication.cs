using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using Candor.Security;
using Infrastructure.Repositories;
using Ninject;
using User = Domain.User;

namespace Infrastructure
{
    public interface IAuthentication
    {
        /// <summary>
        /// Конекст (тут мы получаем доступ к запросу и кукисам)
        /// </summary>
        HttpContext HttpContext { get; set; }

        User Login(string login, string password, bool isPersistent);

        User Login(string login);

        void LogOut();

        IPrincipal CurrentUser { get; }
    }

    public class CustomAuthentication : IAuthentication
    {
        private const string cookieName = "__AUTH_COOKIE";

        public HttpContext HttpContext { get; set; }

        [Inject]
        public IUserRepository Repository { get; set; }

        #region IAuthentication Members

        public User Login(string login, string password, bool isPersistent)
        {
            User retUser = Repository.Login(login, password);
            if (retUser != null)
            {
                CreateCookie(login, isPersistent);
            }
            return retUser;
        }

        public User Login(string login)
        {
            User retUser = Repository.Login(login);
            if (retUser != null)
            {
                CreateCookie(login);
            }
            return retUser;
        }

        private void CreateCookie(string login, bool isPersistent = false)
        {
            var ticket = new FormsAuthenticationTicket(
                  1,
                  login,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var authCookie = new HttpCookie(cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };
            HttpContext.Response.Cookies.Set(authCookie);
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[cookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }

        private IPrincipal _currentUser;

        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null)
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(ticket.Name, Repository);
                        }
                        else
                        {
                            _currentUser = new UserProvider(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Failed authentication: " + ex.Message);
                        _currentUser = new UserProvider(null, null);
                    }
                }
                return _currentUser;
            }
        }
        #endregion
    }

}
