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
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Personals/

        public ViewResult Index()
        {
            return View(unitOfWork.PersonalRepository.All);
        }

        //
        // GET: /Personals/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(unitOfWork.PersonalRepository.Find(id));
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
                unitOfWork.PersonalRepository.InsertOrUpdate(personal);
                unitOfWork.PersonalRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Personals/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            return View(unitOfWork.PersonalRepository.Find(id));
        }

        //
        // POST: /Personals/Edit/5

        [HttpPost]
        public ActionResult Edit(Personal personal)
        {
            if (ModelState.IsValid) {
                unitOfWork.PersonalRepository.InsertOrUpdate(personal);
                unitOfWork.PersonalRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Personals/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(unitOfWork.PersonalRepository.Find(id));
        }

        //
        // POST: /Personals/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            unitOfWork.PersonalRepository.Delete(id);
            unitOfWork.PersonalRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                unitOfWork.PersonalRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

