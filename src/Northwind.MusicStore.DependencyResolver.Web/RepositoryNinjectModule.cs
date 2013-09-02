using Ninject.Modules;
using Ninject.Web.Common;
using Northwind.MusicStore.Data;
using Northwind.MusicStore.Data.Context;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.DependencyResolver.Web
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IDbContextFactory>().To<MusicStoreDbContextFactory>().InRequestScope();
            Bind<IUnitOfWork>().To<MusicStoreUnitOfWork>().InRequestScope();
            
            Bind<IAlbumsRepository>().To<AlbumRepository>().InRequestScope();
            Bind<IArtistRepository>().To<ArtistRepository>().InRequestScope();
            Bind<ICartRepository>().To<CartRepository>().InRequestScope();
            Bind<IGenreRepository>().To<GenreRepository>().InRequestScope();
            Bind<IOrderRepository>().To<OrderRepository>().InRequestScope();
            Bind<IOrderDetailRepository>().To<OrderDetailRepository>().InRequestScope();
        }
        
    }
}