using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Northwind.MusicStore.WebUI.Filters;
using Northwind.MusicStore.RepositoryInterfaces;
using Northwind.MusicStore.BusinessInterfaces;

namespace Northwind.MusicStore.WebUI.Controllers
{

    [LogAction]
    public class StoreController : Controller
    {
        private readonly IGenreRepository _genres;
        private readonly IAlbumsRepository _albums;
        private readonly ILogProvider _log;
        //private readonly ILog _log;

        public StoreController(IGenreRepository genres, IAlbumsRepository albums, ILogProvider log)
        {
            _genres = genres;
            _albums = albums;
            _log = log;
            //_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }
        
        public ActionResult Index()
        {
            Session["HitIndex"] = (int)(Session["HitIndex"] ?? 0) + 1;
            _log.ErrorFormat("The StoreController.Index action has been hit {0} times this session", Session["HitIndex"]);
            
            var genres = _genres.Get().ToList();
            return View(genres);
        }
        
        // GET: /Store/Browse?genre=Disco
        //[LogAction]
        public ActionResult Browse(string genre)
        {
            // Retrieve Genre and its Associated Albums from database
            //var genreModel = storeDB.Genres.Include("Albums")
            //  .Single(g => g.Name == genre);
            var genreModel = _genres.Get()
                .Include(a=>a.Albums)
                .Single(g => g.Name == genre);

            return View(genreModel);
        }
        //
        // GET: /Store/Details/5
        public ActionResult Details(int id)
        {
            //var album = storeDB.Albums.Find(id);
            var album = _albums.Get(a=>a.AlbumId==id)
                .Include(a=>a.Genre)
                .Include(a => a.Artist)
                .SingleOrDefault();

            return View(album);
        }

        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            var genres = _genres.Get().ToList();
            return PartialView(genres);
        }

    }
}
