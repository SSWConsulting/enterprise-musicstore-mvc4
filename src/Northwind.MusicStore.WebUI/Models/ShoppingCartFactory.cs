using System.Web;
using System.Web.Mvc;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.WebUI.Models
{
    public class ShoppingCartFactory : IShoppingCartFactory
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICartRepository _carts;
        private readonly IAlbumsRepository _albums;
        private readonly IOrderDetailRepository _orderDetails;

        public ShoppingCartFactory(IUnitOfWork unitOfWork, ICartRepository carts, IAlbumsRepository albums, IOrderDetailRepository orderDetails)
        {
            _unitOfWork = unitOfWork;
            _carts = carts;
            _albums = albums;
            _orderDetails = orderDetails;
        }
        public ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart(_unitOfWork, _carts, _albums, _orderDetails);
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
    }
}