using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Domain;
using Domain.Events;
using Infrastructure;
using NHibernate;
using NHibernate.Exceptions;
using NHibernate.Linq;
using NHibernate.Transform;

namespace WebApplication2.api
{
    public class DefaultController : ApiController
    {
        // GET: Default

        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public int Get()
        {

            var session = NHibernateHelper.OpenSession();


            return 10;
        }
    }
}