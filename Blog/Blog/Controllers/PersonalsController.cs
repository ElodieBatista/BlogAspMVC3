using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{   
    public class PersonalsController : Controller
    {
		private readonly IPersonalRepository personalRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public PersonalsController() : this(new PersonalRepository())
        {
        }

        public PersonalsController(IPersonalRepository personalRepository)
        {
			this.personalRepository = personalRepository;
        }

        //
        // GET: /Personals/

        public ViewResult Index()
        {
            return View(personalRepository.All);
        }

        //
        // GET: /Personals/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(personalRepository.Find(id));
        }

        //
        // GET: /Personals/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Personals/Create

        [HttpPost]
        public ActionResult Create(Personal personal)
        {
            if (ModelState.IsValid) {
                personalRepository.InsertOrUpdate(personal);
                personalRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Personals/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
             return View(personalRepository.Find(id));
        }

        //
        // POST: /Personals/Edit/5

        [HttpPost]
        public ActionResult Edit(Personal personal)
        {
            if (ModelState.IsValid) {
                personalRepository.InsertOrUpdate(personal);
                personalRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Personals/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(personalRepository.Find(id));
        }

        //
        // POST: /Personals/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            personalRepository.Delete(id);
            personalRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                personalRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

