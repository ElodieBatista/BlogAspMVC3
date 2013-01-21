using Blog.Models;
using CodeFirstMembershipSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Blog.Repositories
{ 
    public class CommentRepository : ICommentRepository
    {
        private DataContext context;
        public CommentRepository(DataContext context)
        {
            this.context = context;
        }


        public IQueryable<Comment> All
        {
            get { return context.Comments; }
        }

        public IQueryable<Comment> AllIncluding(params Expression<Func<Comment, object>>[] includeProperties)
        {
            IQueryable<Comment> query = context.Comments;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Comment Find(System.Guid id)
        {
            return context.Comments.Find(id);
        }

        public void InsertOrUpdate(Comment comment)
        {
            if (comment.Id == default(System.Guid)) {
                // New entity
                comment.Id = Guid.NewGuid();
                context.Comments.Add(comment);
            } else {
                // Existing entity
                context.Entry(comment).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var comment = context.Comments.Find(id);
            context.Comments.Remove(comment);
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

    public interface ICommentRepository : IDisposable
    {
        IQueryable<Comment> All { get; }
        IQueryable<Comment> AllIncluding(params Expression<Func<Comment, object>>[] includeProperties);
        Comment Find(System.Guid id);
        void InsertOrUpdate(Comment comment);
        void Delete(System.Guid id);
        void Save();
    }
}