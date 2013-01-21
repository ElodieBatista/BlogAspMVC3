using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Common
{
    public class PutDataActionFilter : ActionFilterAttribute
    {   
        // Auto Fill of some information
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var unitOfWork = new UnitOfWork();
            var personalRepo = unitOfWork.PersonalRepository;

            // Get all Personal information & fill ViewBag
            Personal myPersonal = personalRepo.All.First();
            filterContext.Controller.ViewBag.PersonalFirstName = myPersonal.Firstname;
            filterContext.Controller.ViewBag.PersonalLastName = myPersonal.Lastname;
            filterContext.Controller.ViewBag.PersonalTitle = myPersonal.Title;
            filterContext.Controller.ViewBag.PersonalDescription = myPersonal.Description;
            filterContext.Controller.ViewBag.Title = Blog.Resources.Res.BlogOf + myPersonal.Firstname + myPersonal.Lastname;
        }
    }
}