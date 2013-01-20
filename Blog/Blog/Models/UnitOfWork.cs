using Blog.Context;
using Blog.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class UnitOfWork: IDisposable
    {
        private DataContext context = new DataContext();
        private PostRepository postRepository;
        private CommentRepository commentRepository;
        private PersonalRepository personalRepository;

        public PostRepository PostRepository
        {
            get
            {
                if (this.postRepository == null)
                {
                    this.postRepository = new PostRepository(context);
                }
                return postRepository;
            }
        }

        public CommentRepository CommentRepository
        {
            get
            {
                if (this.commentRepository == null)
                {
                    this.commentRepository = new CommentRepository(context);
                }
                return commentRepository;
            }
        }

        public PersonalRepository PersonalRepository
        {
            get
            {
                if (this.personalRepository == null)
                {
                    this.personalRepository = new PersonalRepository(context);
                }
                return personalRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}