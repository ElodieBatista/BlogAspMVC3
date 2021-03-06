﻿using Blog.Common;
using CodeFirstMembershipSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            // Register our custom Filter
            filters.Add(new PutDataActionFilter());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "LocalizedDefault", // Route name
                "{lang}/{controller}/{action}/{id}", // URL with parameters
                new { lang = "fr-FR", controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "Default", // Route name
                "{lang}/{controller}/{action}/{id}", // URL with parameters
                new { lang = "fr-FR", controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            /*routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );*/

        }

        protected void Application_Start()
        {
            // Create DB
            Database.SetInitializer(new DataContextInitializer());

            // Connection to DB and Test
            using (var db = new DataContext())
            {
                var query = from post in db.Posts
                            select post;
                var list = query.ToList();
            }

            
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}