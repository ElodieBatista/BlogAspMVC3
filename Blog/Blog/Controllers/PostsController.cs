using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.ViewModel;

namespace Blog.Controllers
{   
    public class PostsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Posts/

        public ViewResult Index(int? page)
        {
            // 10 posts per page
            const int pageSize = 10;

            // Get all posts ordered by date
            var postList = unitOfWork.PostRepository
                                        .AllIncluding(post => post.Comments)
                                        .OrderByDescending(i => i.Posted);

            // Number of Posts
            int postCount = postList.Count();

            // Offset wanted
            int skip = ((page ?? 0) * pageSize);
            if (skip > 0)
                skip -= pageSize;

            // Get the 10 posts from the offset
            var tenPosts = postList.Skip(skip).Take(pageSize).ToList();

            ViewBag.CurrentPage = (page ?? 0);

            // If there are more older
            ViewBag.HasPrevious = (skip + pageSize < postCount);

            // If there are more younger
            ViewBag.HasMore = (skip > 0);

            // If current pafe is 0
            if (ViewBag.CurrentPage == 0) {
                // Page 2
                ViewBag.PreviousPage = (ViewBag.CurrentPage - 1) * (-1) + 1;
            } else {
                // Previous page with previous posts
                ViewBag.PreviousPage = (ViewBag.CurrentPage + 1);

                // Next page with recent posts
                ViewBag.FuturPage = (ViewBag.CurrentPage - 1);
            }

            return View(tenPosts);
        }

        //
        // GET: /Posts/Details/5

        public ViewResult Details(System.Guid id)
        {
            var vm = new DetailsPostViewModel();

            // Get the Post with all its Comments ordered by date
            vm.Post = unitOfWork.PostRepository.Find(id);
            vm.Post.Comments = unitOfWork.CommentRepository
                                                .All
                                                .Where(comment => comment.PostId == id)
                                                .OrderByDescending(comment => comment.Posted)
                                                .ToList();

            // New Comment for the form
            vm.VMCreateComment = new CreateCommentViewModel();
            vm.VMCreateComment.Comment = new Comment();
            vm.VMCreateComment.Comment.PostId = id;
            vm.VMCreateComment.Comment.Posted = DateTime.Now;

            // Get the most recent Post just after this one
            var postMoreRecent = unitOfWork.PostRepository
                                            .All
                                            .Where(post => post.Posted > vm.Post.Posted)
                                            .OrderBy(post => post.Posted)
                                            .FirstOrDefault();

            // If this Post is the most recent
            if (postMoreRecent == null) {
                ViewBag.HasRecent = false;
            } else {
                ViewBag.HasRecent = true;
                vm.PostRecent = postMoreRecent;
            }

            // Get the oldest Post just after this one
            var postOlder = unitOfWork.PostRepository
                                        .All
                                        .Where(post => post.Posted < vm.Post.Posted)
                                        .OrderByDescending(post => post.Posted)
                                        .FirstOrDefault();

            // If this Post is the oldest
            if (postOlder == null) {
                ViewBag.HasOlder = false;
            } else {
                ViewBag.HasOlder = true;
                vm.PostOlder = postOlder;
            }

            // The Post for this Comment is already known so no dropdown list to choose the Post
            vm.VMCreateComment.CreateEntire = false;

            // Facebook Like Button
            ViewBag.UrlToLike = "//www.facebook.com/plugins/like.php?href=http%3A%2F%2F" 
                                + Request.RawUrl 
                                + "&amp;send=false&amp;layout=button_count&amp;width=450&amp;show_faces=false&amp;action=like&amp;colorscheme=light&amp;font=verdana&amp;height=21";

            return View(vm);
        }

        //
        // GET: /Posts/Create

        public ActionResult Create()
        {
            // Post instanciation with date pre-filled
            var newPost = new Post();
            newPost.Posted = DateTime.Now;

            return View(newPost);
        } 

        //
        // POST: /Posts/Create

        [HttpPost]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid) {
                // Update date
                post.Posted = DateTime.Now;
                unitOfWork.PostRepository.InsertOrUpdate(post);
                unitOfWork.PostRepository.Save();
                return RedirectToAction("Index");
            } else {
                return View();
            }
        }
        
        //
        // GET: /Posts/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            return View(unitOfWork.PostRepository.Find(id));
        }

        //
        // POST: /Posts/Edit/5

        [HttpPost]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid) {
                unitOfWork.PostRepository.InsertOrUpdate(post);
                unitOfWork.PostRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Posts/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(unitOfWork.PostRepository.Find(id));
        }

        //
        // POST: /Posts/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            unitOfWork.PostRepository.Delete(id);
            unitOfWork.PostRepository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                unitOfWork.PostRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}