using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{   
    public class CommentsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Comments/

        public ViewResult Index()
        {
            return View(unitOfWork.CommentRepository.AllIncluding(comment => comment.Post));
        }

        //
        // GET: /Comments/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(unitOfWork.CommentRepository.Find(id));
        }

        //
        // GET: /Comments/Create

        public ActionResult Create()
        {
            ViewBag.PossiblePosts = unitOfWork.PostRepository.All;
            return View();
        } 

        //
        // POST: /Comments/Create

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid) {
                unitOfWork.CommentRepository.InsertOrUpdate(comment);
                unitOfWork.CommentRepository.Save();
                return RedirectToAction("Index");
            } else {
                ViewBag.PossiblePosts = unitOfWork.PostRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Comments/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            ViewBag.PossiblePosts = unitOfWork.PostRepository.All;
            return View(unitOfWork.CommentRepository.Find(id));
        }

        //
        // POST: /Comments/Edit/5

        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid) {
                unitOfWork.CommentRepository.InsertOrUpdate(comment);
                unitOfWork.CommentRepository.Save();
                return RedirectToAction("Index");
            } else {
                ViewBag.PossiblePosts = unitOfWork.PostRepository.All;
				return View();
			}
        }

        //
        // GET: /Comments/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(unitOfWork.CommentRepository.Find(id));
        }

        //
        // POST: /Comments/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            unitOfWork.CommentRepository.Delete(id);
            unitOfWork.CommentRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                unitOfWork.PostRepository.Dispose();
                unitOfWork.CommentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

