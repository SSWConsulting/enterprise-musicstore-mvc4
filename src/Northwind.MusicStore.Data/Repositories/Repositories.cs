using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.MusicStore.Data.Context;
using Northwind.MusicStore.Domain;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.Data
{

    public class AlbumRepository : RepositoryBase<Album>, IAlbumsRepository
    {
        public AlbumRepository(IDbContextFactory contextFactory) : base(contextFactory) { }

        public IEnumerable<Album> GetTopSellingAlbums(int count)
        {
            return Get()
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }
    }

    public class ArtistRepository : RepositoryBase<Artist>, IArtistRepository
    {
        public ArtistRepository(IDbContextFactory contextFactory) : base(contextFactory) { }
    }
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(IDbContextFactory contextFactory) : base(contextFactory) { }
    }
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(IDbContextFactory contextFactory) : base(contextFactory) { }
    }
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbContextFactory contextFactory) : base(contextFactory) { }
    }
    public class OrderDetailRepository : RepositoryBase<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(IDbContextFactory contextFactory) : base(contextFactory) { }
    }
}
