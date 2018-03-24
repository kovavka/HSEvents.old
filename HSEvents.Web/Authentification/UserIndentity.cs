using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Domain;
using Infrastructure.Repositories;
using Ninject;

namespace HSEvents.Web.Authentification
{
    public interface IUserProvider
    {
        User User { get; set; }
    }

    public class UserIndentity : IIdentity, IUserProvider
    {
        [Inject]
        public IAuthentication Auth { get; set; }

        public User CurrentUser
        {
            get
            {
                return ((IUserProvider)Auth.CurrentUser.Identity).User;
            }
        }

        public User User { get; set; }

        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Login;
                }
                //иначе аноним
                return "anonym";
            }
        }

        public void Init(string login, IUserRepository repository)
        {
            if (!string.IsNullOrEmpty(login))
            {
                User = repository.Login(login);
            }
        }
    }
}