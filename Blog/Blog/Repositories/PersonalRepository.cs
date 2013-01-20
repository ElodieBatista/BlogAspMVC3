using Blog.Context;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Blog.Repositories
{ 
    public class PersonalRepository : IPersonalRepository
    {
        private DataContext context;
        public PersonalRepository(DataContext context)
        {
            this.context = context;
        }

        public IQueryable<Personal> All
        {
            get { return context.Personals; }
        }

        public IQueryable<Personal> AllIncluding(params Expression<Func<Personal, object>>[] includeProperties)
        {
            IQueryable<Personal> query = context.Personals;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public Personal Find(System.Guid id)
        {
            return context.Personals.Find(id);
        }

        public void InsertOrUpdate(Personal personal)
        {
            if (personal.Id == default(System.Guid)) {
                // New entity
                personal.Id = Guid.NewGuid();
                context.Personals.Add(personal);
            } else {
                // Existing entity
                context.Entry(personal).State = EntityState.Modified;
            }
        }

        public void Delete(System.Guid id)
        {
            var personal = context.Personals.Find(id);
            context.Personals.Remove(personal);
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

    public interface IPersonalRepository : IDisposable
    {
        IQueryable<Personal> All { get; }
        IQueryable<Personal> AllIncluding(params Expression<Func<Personal, object>>[] includeProperties);
        Personal Find(System.Guid id);
        void InsertOrUpdate(Personal personal);
        void Delete(System.Guid id);
        void Save();
    }
}