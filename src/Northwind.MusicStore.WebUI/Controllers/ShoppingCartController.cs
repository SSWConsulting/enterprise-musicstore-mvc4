using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Northwind.MusicStore.RepositoryInterfaces;
using Northwind.MusicStore.WebUI.Models;
using Northwind.MusicStore.WebUI.ViewModels;

namespace MvcMusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly ICartRepository _carts;
        private readonly IAlbumsRepository _albums;
        private readonly ShoppingCartFactory _shoppingCartFactory;
        //MusicStoreEntities storeDB = new MusicStoreEntities();

        public ShoppingCartController(ICartRepository carts, IAlbumsRepository albums, ShoppingCartFactory shoppingCartFactory)
        {
            _carts = carts;
            _albums = albums;
            _shoppingCartFactory = shoppingCartFactory;
        }

        //
        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = _shoppingCartFactory.GetCart(this.HttpContext);

            // Configure our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };
            // Return the view
            return View(viewModel);
        }
        //
        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            //var addedAlbum = storeDB.Albums
            //    .Single(album => album.AlbumId == id);
            var addedAlbum = _albums.Get(album => album.AlbumId == id).Single();

            // Add it to the shopping cart
            var cart = _shoppingCartFactory.GetCart(this.HttpContext);

            cart.AddToCart(addedAlbum);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }
        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = _shoppingCartFactory.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            //string albumName = storeDB.Carts.Single(item => item.RecordId == id).Album.Title;
            string albumName = _carts.Get().Include(c=>c.Album).Single(item => item.RecordId == id).Album.Title;

            // Remove from cart
            int itemCount = cart.RemoveFromCart(id);

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) +
                    " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };
            return Json(results);
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = _shoppingCartFactory.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();
            return PartialView("CartSummary");
        }
    }

}
