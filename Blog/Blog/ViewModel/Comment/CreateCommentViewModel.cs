using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.ViewModel
{
    public class CreateCommentViewModel
    {
        public Comment Comment { get; set; }
        public bool CreateEntire { get; set; }
        public List<Post> PossiblePosts { get; set; }
    }
}
