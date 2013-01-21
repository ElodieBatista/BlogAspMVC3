using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using Blog.ViewModel;

namespace Blog.Controllers
{   
    public class CommentsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Comments/

        public ViewResult Index(int? page)
        {
            // 20 comments per page
            const int pageSize = 20;

            // Get all comments ordered by date
            var commentsList = unitOfWork.CommentRepository
                                            .AllIncluding(comment => comment.Post)
                                            .OrderByDescending(comment => comment.Posted);

            // Number of comments found
            int commentCount = commentsList.Count();

            // Offset wanted
            int skip = ((page ?? 0) * pageSize);
            if (skip > 0)
                skip -= pageSize;

            // Get 20 comments from the offset
            var twentyComments = commentsList.Skip(skip).Take(pageSize).ToList();

            ViewBag.CurrentPage = (page ?? 0);

            // If there are more older
            ViewBag.HasPrevious = (skip + pageSize < commentCount);

            // If there are more younger
            ViewBag.HasMore = (skip > 0);

            // If current page is 0
            if (ViewBag.CurrentPage == 0) {
                // Page 2
                ViewBag.PreviousPage = (ViewBag.CurrentPage - 1) * (-1) + 1;
            } else {
                // Previous Page with previous posts
                ViewBag.PreviousPage = (ViewBag.CurrentPage + 1);

                // Next Page with recent posts
                ViewBag.FuturPage = (ViewBag.CurrentPage - 1);
            }

            return View(twentyComments);
        }

        //
        // GET: /Comments/Details/5

        public ViewResult Details(System.Guid id)
        {
            // Get the Comment with its Post
            return View(unitOfWork.CommentRepository
                                    .AllIncluding(comment => comment.Post)
                                    .Where(comment => comment.Id == id)
                                    .First());
        }

        //
        // GET: /Comments/Create

        public ActionResult Create()
        {
            var vm = new CreateCommentViewModel();

            // Dropdown list to choose the Post for this Comment
            vm.CreateEntire = true;
            vm.PossiblePosts = unitOfWork.PostRepository
                                            .All
                                            .ToList();

            // Comment for the form
            vm.Comment = new Comment();
            vm.Comment.Posted = DateTime.Now;
            vm.Comment.Post = new Post();
            
            return View(vm);
        } 

        //
        // POST: /Comments/Create

        [HttpPost]
        public ActionResult Create(CreateCommentViewModel vm)
        {
            if (ModelState.IsValid) {
                vm.Comment.Posted = DateTime.Now;
                unitOfWork.CommentRepository.InsertOrUpdate(vm.Comment);
                unitOfWork.CommentRepository.Save();
                string url = "/../Posts/Details/" + vm.Comment.PostId;

                return RedirectToAction(url);
            } else {
                // Dropdown list to choose the Post for this Comment
                vm.CreateEntire = true;
                vm.PossiblePosts = unitOfWork.PostRepository
                                                .All
                                                .ToList();

                // Comment for the form
                vm.Comment = new Comment();
                vm.Comment.Posted = DateTime.Now;
                vm.Comment.Post = new Post();

                return View(vm);
            }
        }

        //
        // GET: /Comments/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            // Get the Comment with its Post
            return View(unitOfWork.CommentRepository
                                    .AllIncluding(comment => comment.Post)
                                    .Where(comment => comment.Id == id)
                                    .First());
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