using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using Northwind.MusicStore.Domain;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.WebUI.Models
{
    public partial class ShoppingCart
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _carts;
        private readonly IAlbumsRepository _albums;
        private readonly IOrderDetailRepository _orderDetails;
        //MusicStoreEntities storeDB = new MusicStoreEntities();

        public ShoppingCart(IUnitOfWork unitOfWork, ICartRepository carts, IAlbumsRepository albums, IOrderDetailRepository orderDetails)
        {
            _unitOfWork = unitOfWork;
            _carts = carts;
            _albums = albums;
            _orderDetails = orderDetails;
        }

        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";

        public void AddToCart(Album album)
        {
            // Get the matching cart and album instances
            var cartItem = _carts.Get().SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            _unitOfWork.SaveChanges();
        }
        public int RemoveFromCart(int id)
        {
            // Get the cart
            var cartItem = _carts.Get().Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    _carts.Delete(cartItem);
                }
                // Save changes
                _unitOfWork.SaveChanges();
            }
            return itemCount;
        }
        public void EmptyCart()
        {
            var cartItems = _carts.Get().Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                _carts.Delete(cartItem);
            }
            // Save changes
            _unitOfWork.SaveChanges();
        }
        public List<Cart> GetCartItems()
        {
            List<Cart> cartItems = _carts.Get()
                .Include(c => c.Album)
                .Where(cart => cart.CartId == ShoppingCartId)

                .ToList();

            return cartItems;
        }
        public int GetCount()
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in _carts.Get()
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;

        }
        public decimal GetTotal()
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in _carts.Get()
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Album.Price).Sum();

            return total ?? decimal.Zero;
        }
        public int CreateOrder(Order order)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems();
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Album.Price);

                _orderDetails.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            _unitOfWork.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            var shoppingCart = _carts.Get().Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            _unitOfWork.SaveChanges();
        }
    }
}
