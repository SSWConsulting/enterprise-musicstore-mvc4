using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Northwind.MusicStore.Data.Context;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.Data
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private DbContext _context;
        private IDbSet<T> _dbSet;

        protected RepositoryBase(IDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            ContextFactory = contextFactory;
            Context.Configuration.ProxyCreationEnabled = false;
            _dbSet = Context.Set<T>();
        }

        protected IDbContextFactory ContextFactory { get; private set; }

        protected DbContext Context
        {
            get { return _context ?? (_context = ContextFactory.Get()); }
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Where(filter);
        }

        public IQueryable<T> Get()
        {
            return _dbSet;
        }

        public T Find(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public void Update(T entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}