using Northwind.MusicStore.Data.EntityConfig;

namespace Northwind.MusicStore.Data.Context
{
    public class MusicStoreDbContextFactory : IDbContextFactory
    {
        private MusicStoreDbContext _context;

        public MusicStoreDbContext Get()
        {
            if (_context == null) InitialiseContext();

            return _context;
        }

        private void InitialiseContext()
        {

            //When Using SQLDeploy, we want to update the database ourselves.
            //System.Data.Entity.Database.SetInitializer<MusicStoreDbContext>(null);

            //To Use CodeFirst and have it create the sample data ..  this initialiser will create the database and insert sample data.
            System.Data.Entity.Database.SetInitializer<MusicStoreDbContext>(new MusicStoreDbInitializer());
            //once up and running, use the following
            //System.Data.Entity.Database.SetInitializer<MusicStoreDbContext>(null);

            //this is a sample of an alternative method
            //System.Data.Entity.Database.SetInitializer<MusicStoreEntities>(new CreateDatabaseIfNotExists<MusicStoreEntities>());

            _context = new MusicStoreDbContext();
        }
    }
}
