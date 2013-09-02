using System;
using System.Linq;
using System.Web.Mvc;
using Northwind.MusicStore.WebUI.Models;
using Northwind.MusicStore.Domain;
using Northwind.MusicStore.RepositoryInterfaces;

namespace Northwind.MusicStore.WebUI.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ShoppingCartFactory _shoppingCartFactory;
        private readonly IOrderRepository _orders;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutController(ShoppingCartFactory shoppingCartFactory, IOrderRepository orderRepository, IUnitOfWork unitOfWork  )
        {
            _shoppingCartFactory = shoppingCartFactory;
            _orders = orderRepository;
            _unitOfWork = unitOfWork;
        }

        const string PromoCode = "FREE";
        //
        // GET: /Checkout/AddressAndPayment
        public ActionResult AddressAndPayment()
        {
            return View();
        }
        //
        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        public ActionResult AddressAndPayment(FormCollection values)
        {
            var order = new Order();
            TryUpdateModel(order);

            try
            {
                if (string.Equals(values["PromoCode"], PromoCode,
                    StringComparison.OrdinalIgnoreCase) == false)
                {
                    return View(order);
                }
                else
                {
                    order.Username = User.Identity.Name;
                    order.OrderDate = DateTime.Now;

                    //Save Order
                    _orders.Add(order);
                    _unitOfWork.SaveChanges();
                    //Process the order
                    var cart = _shoppingCartFactory.GetCart(this.HttpContext);
                    cart.CreateOrder(order);

                    return RedirectToAction("Complete",
                        new { id = order.OrderId });
                }
            }
            catch
            {
                //Invalid - redisplay with errors
                return View(order);
            }
        }
        //
        // GET: /Checkout/Complete
        public ActionResult Complete(int id)
        {
            // Validate customer owns this order
            bool isValid = _orders.Get().Any(
                o => o.OrderId == id &&
                o.Username == User.Identity.Name);

            if (isValid)
            {
                return View(id);
            }
            else
            {
                return View("Error");
            }
        }
    }
}