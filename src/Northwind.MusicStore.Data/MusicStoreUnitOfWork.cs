using System;
using Northwind.MusicStore.Data.Context;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.Data
{
    public class MusicStoreUnitOfWork : IUnitOfWork
    {
        private IDbContextFactory _contextFactory;
        private MusicStoreDbContext _context;

        public MusicStoreUnitOfWork(IDbContextFactory contextFactory)
        {
            if (contextFactory == null)
            {
                throw new ArgumentNullException("contextFactory");
            }

            _contextFactory = contextFactory;
        }

        protected MusicStoreDbContext Context
        {
            get { return _context ?? (_context = _contextFactory.Get()); }
        }

        public void SaveChanges()
        {
            Context.Commit();
        }
    }
}
