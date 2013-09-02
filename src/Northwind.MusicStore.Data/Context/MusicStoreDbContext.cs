using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Northwind.MusicStore.Domain;

namespace Northwind.MusicStore.Data.Context
{
    public class MusicStoreDbContext : DbContext
    {
        public IDbSet<Album> Albums { get; set; }
        public IDbSet<Artist> Artists { get; set; }
        public IDbSet<Cart> Carts { get; set; }
        public IDbSet<Genre> Genres { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderDetail> OrderDetails { get; set; }

        public MusicStoreDbContext()
            : base("name=MusicStoreEntities")
        {
        }

        public MusicStoreDbContext(string connectionString)
            : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            //modelBuilder.Entity<Album>().ToTable("Albums");
            //modelBuilder.Configurations.Add(new AlbumConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public void Commit()
        {
            base.SaveChanges();
        }
    }
}