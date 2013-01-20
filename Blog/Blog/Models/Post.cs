﻿using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Resources;

namespace Blog.Models
{
    public class Post
    {
        // Primary Key Auto Generated by DB and not display in forms
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput(DisplayValue = false)]
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Res), ErrorMessageResourceName = "RequiredTitle")]
        [Display(Name = "Title", ResourceType = typeof(Res))]
        public string Title { get; set; }

        // Textarea
        [Required(ErrorMessageResourceType = typeof(Res), ErrorMessageResourceName = "RequiredPost")]
        [Display(Name = "Post", ResourceType = typeof(Res))]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Date]
        [HiddenInput(DisplayValue = false)]
        public DateTime Posted { get; set; }

        // Relations
        public virtual ICollection<Comment> Comments { get; set; }
    }
}