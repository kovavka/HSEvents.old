using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;
using Domain;
using Common;
using Infrastructure.Repositories;
using Unity.Attributes;

namespace HSEvents.Web.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login(string login = null)
        {
            if (CurrentUser != null)
                return RedirectToAction("Index", "Events");

            if (login.IsNullOrEmpty())
                return View();

            return View(new User() {Login = login});
        }

        [HttpPost]
        public ActionResult Login(User user, bool rememberMe)
        {
            if (user.Login.IsNullOrEmpty())
                ModelState.AddModelError("Login", "Введите логин");
            if (user.Password.IsNullOrEmpty())
                ModelState.AddModelError("Password", "Введите пароль");

            if (!ModelState.IsValid)
                return View();
            
            Auth.Login(user.Login, user.Password, rememberMe);
            if (CurrentUser!= null)
                return RedirectToAction("Index", "Events");

            ModelState.AddModelError("Button", "Неверный логин или пароль");
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (CurrentUser != null)
                return RedirectToAction("Index", "Events");

            return View();
        }

        [Dependency]
        public IUserRepository repository {get; set; } 

        [HttpPost]
        public ActionResult Register(User user, string confirmPassword)
        {
            if (user.Login.IsNullOrEmpty())
                ModelState.AddModelError("Login", "Введите логин");
            if (user.Login.IsNotEmpty() && repository.Exists(user.Login))
                ModelState.AddModelError("Login", "Такой логин уже существует");
            if (user.Password.IsNullOrEmpty())
                ModelState.AddModelError("Password", "Введите пароль");
            if (confirmPassword.IsNullOrEmpty())
                ModelState.AddModelError("confirmPassword", "Повторите пароль");
            if (confirmPassword.IsNotEmpty() && user.Password.IsNotEmpty() && confirmPassword != user.Password)
            {
                ModelState.AddModelError("Password", "Пароли не совпадают");
                ModelState.AddModelError("confirmPassword", "Пароли не совпадают");
            }
            if (user.Appointment.IsNullOrEmpty())
                ModelState.AddModelError("Appointment", "Введите должность");
            if (user.ContactInfo.FullName.IsNullOrEmpty())
                ModelState.AddModelError("FullName", "Введите ФИО");
            if (!user.ContactInfo.Email.IsCorrectEmail())
                ModelState.AddModelError("Email", "Неверный формат Email");
            if (!user.ContactInfo.PhoneNumber.IsCorrectPhone())
                ModelState.AddModelError("PhoneNumber", "Неверный формат номера");

            if (!ModelState.IsValid)
                return View();

            repository.Add(user);

            return RedirectToAction("Login", "Account", new {login = user.Login});
        }

        public ActionResult Logout()
        {
            Auth.LogOut();
            return RedirectToAction("Index", "Home");
        }
    }
}