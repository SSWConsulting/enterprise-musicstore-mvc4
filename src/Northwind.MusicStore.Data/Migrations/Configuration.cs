using System.Data.Entity.Migrations;
using Northwind.MusicStore.Data.Context;
using Northwind.MusicStore.Data.EntityConfig;

namespace Northwind.MusicStore.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MusicStoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MusicStoreDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var initializer = new MusicStoreDbInitializer();
            initializer.MigrationsSeed(context);

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
