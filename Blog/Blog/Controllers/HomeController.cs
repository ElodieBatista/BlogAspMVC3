using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /Home/

        public ActionResult Index(int? page)
        {
            const int pageSize = 10;

            // Get all Posts ordered by date
            var postList = unitOfWork.PostRepository
                                        .AllIncluding(post => post.Comments)
                                        .OrderByDescending(i => i.Posted);

            // Number of Posts
            int postCount = postList.Count();

            // Offset wanted
            int skip = ((page ?? 0) * pageSize);

            if (skip > 0)
                skip -= pageSize;

            // Get 10 Posts from offset
            var tenPosts = postList.Skip(skip).Take(pageSize).ToList();

            ViewBag.CurrentPage = (page ?? 0);

            // If there are more older
            ViewBag.HasPrevious = (skip + pageSize < postCount);

            // If there are more younger
            ViewBag.HasMore = (skip > 0);

            // If current page is 0
            if (ViewBag.CurrentPage == 0)
            {
                // Page 2
                ViewBag.PreviousPage = (ViewBag.CurrentPage - 1) * (-1) + 1;
            }
            else
            {
                // Previous page with previous posts
                ViewBag.PreviousPage = (ViewBag.CurrentPage + 1);

                // Next page with recent posts
                ViewBag.FuturPage = (ViewBag.CurrentPage - 1);
            }

            return View(tenPosts);
        }
    }
}