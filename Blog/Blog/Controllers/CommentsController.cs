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
		private readonly IPostRepository postRepository;
		private readonly ICommentRepository commentRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public CommentsController() : this(new PostRepository(), new CommentRepository())
        {
        }

        public CommentsController(IPostRepository postRepository, ICommentRepository commentRepository)
        {
			this.postRepository = postRepository;
			this.commentRepository = commentRepository;
        }

        //
        // GET: /Comments/

        public ViewResult Index()
        {
            return View(commentRepository.AllIncluding(comment => comment.Post));
        }

        //
        // GET: /Comments/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(commentRepository.Find(id));
        }

        //
        // GET: /Comments/Create

        public ActionResult Create()
        {
			ViewBag.PossiblePosts = postRepository.All;
            return View();
        } 

        //
        // POST: /Comments/Create

        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            if (ModelState.IsValid) {
                commentRepository.InsertOrUpdate(comment);
                commentRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossiblePosts = postRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Comments/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
			ViewBag.PossiblePosts = postRepository.All;
             return View(commentRepository.Find(id));
        }

        //
        // POST: /Comments/Edit/5

        [HttpPost]
        public ActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid) {
                commentRepository.InsertOrUpdate(comment);
                commentRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossiblePosts = postRepository.All;
				return View();
			}
        }

        //
        // GET: /Comments/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(commentRepository.Find(id));
        }

        //
        // POST: /Comments/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            commentRepository.Delete(id);
            commentRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                postRepository.Dispose();
                commentRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

