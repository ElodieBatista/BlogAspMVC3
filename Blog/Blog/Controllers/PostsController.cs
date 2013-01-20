using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{   
    public class PostsController : Controller
    {
		private readonly IPostRepository postRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public PostsController() : this(new PostRepository())
        {
        }

        public PostsController(IPostRepository postRepository)
        {
			this.postRepository = postRepository;
        }

        //
        // GET: /Posts/

        public ViewResult Index()
        {
            return View(postRepository.AllIncluding(post => post.Comments));
        }

        //
        // GET: /Posts/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(postRepository.Find(id));
        }

        //
        // GET: /Posts/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Posts/Create

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid) {
                postRepository.InsertOrUpdate(post);
                postRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Posts/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
             return View(postRepository.Find(id));
        }

        //
        // POST: /Posts/Edit/5

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid) {
                postRepository.InsertOrUpdate(post);
                postRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Posts/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(postRepository.Find(id));
        }

        //
        // POST: /Posts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            postRepository.Delete(id);
            postRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                postRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

