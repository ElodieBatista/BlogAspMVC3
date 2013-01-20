using Blog.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Blog.Models
{ 
    public class PostRepository : IPostRepository
    {
        BlogContext context = new BlogContext();

        public IQueryable<Post> All
        {
            get { return context.Posts; }
        }

        public IQueryable<Post> AllIncluding(params Expression<Func<Post, object>>[] includeProperties)
        {
            IQueryable<Post> query = context.Posts;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Post Find(System.Guid id)
        {
            return context.Posts.Find(id);
        }

        public void InsertOrUpdate(Post post)
        {
            if (post.Id == default(System.Guid)) {
                // New entity
                post.Id = Guid.NewGuid();
                context.Posts.Add(post);
            } else {
                // Existing entity
                context.Entry(post).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var post = context.Posts.Find(id);
            context.Posts.Remove(post);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IPostRepository : IDisposable
    {
        IQueryable<Post> All { get; }
        IQueryable<Post> AllIncluding(params Expression<Func<Post, object>>[] includeProperties);
        Post Find(System.Guid id);
        void InsertOrUpdate(Post post);
        void Delete(System.Guid id);
        void Save();
    }
}