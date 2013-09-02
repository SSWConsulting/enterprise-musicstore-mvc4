using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Northwind.MusicStore.Domain;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        //MusicStoreEntities storeDB = new MusicStoreEntities();
        private readonly IAlbumsRepository _albumsRepository;

        public HomeController(IAlbumsRepository albumsRepository)
        {
            _albumsRepository = albumsRepository;
        }
        
        public ActionResult Index()
        {
            var albums = GetTopSellingAlbums(5);
            
            return View(albums);
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            //Calling the database
            //return storeDB
            //    .OrderByDescending(a => a.OrderDetails.Count())
            //    .Take(count)
            //    .ToList();

            //Calling the repository
            //return _albumsRepository.Get()
            //    .OrderByDescending(a => a.OrderDetails.Count())
            //    .Take(count)
            //    .ToList();

            //encapsulating the logic in the repository
            return _albumsRepository.GetTopSellingAlbums(count).ToList();
            
        }
    }

}
