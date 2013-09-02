using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Northwind.MusicStore.Domain;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.WebUI.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenreRepository _genres;
        private readonly IAlbumsRepository _albums;
        private readonly IArtistRepository _artists;
        //private MusicStoreEntities db = new MusicStoreEntities();

        public StoreManagerController(IUnitOfWork unitOfWork, IGenreRepository genres, IAlbumsRepository albums, IArtistRepository artists)
        {
            _unitOfWork = unitOfWork;
            _genres = genres;
            _albums = albums;
            _artists = artists;
        }
        
        public ActionResult Index()
        {
                      //var albums = db.Albums.Include(a => a.Genre).Include(a => a.Artist);
            List<Album> albums = _albums.Get().Include(a => a.Genre).Include(a => a.Artist).ToList();
            
            return View(albums);
        }

        public ActionResult Details(int id = 0)
        {
            //var album = db.Albums.Find(id);
            var album = _albums.Get(a => a.AlbumId == id).SingleOrDefault();
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(_genres.Get(), "GenreId", "Name");
            ViewBag.ArtistId = new SelectList(_artists.Get(), "ArtistId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                
                //db.Albums.Add(album);
                //db.SaveChanges();
                _albums.Add(album);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(_genres.Get(), "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_artists.Get(), "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        public ActionResult Edit(int id = 0)
        {
            Album album = _albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(_genres.Get(), "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_artists.Get(), "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(album).State = EntityState.Modified;
                //db.SaveChanges();
                _albums.Update(album);
                _unitOfWork.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(_genres.Get(), "GenreId", "Name", album.GenreId);
            ViewBag.ArtistId = new SelectList(_artists.Get(), "ArtistId", "Name", album.ArtistId);
            return View(album);
        }

        public ActionResult Delete(int id = 0)
        {
            Album album = _albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }


        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = _albums.Find(id);
            //_albums.Remove(album);
            //db.SaveChanges();
            _albums.Delete(album);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }

        //no need to call dispose now as Ninject disposes of our objects when they are out of scope.
        protected override void Dispose(bool disposing)
        {
            //db.Dispose();
            base.Dispose(disposing);
        }
    }
}