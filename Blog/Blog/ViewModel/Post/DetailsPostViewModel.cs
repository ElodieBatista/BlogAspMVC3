using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.ViewModel
{
    public class DetailsPostViewModel
    {
        public Post Post { get; set; }
        public Post PostRecent { get; set; }
        public Post PostOlder { get; set; }
        public CreateCommentViewModel VMCreateComment { get; set; }
    }
}
