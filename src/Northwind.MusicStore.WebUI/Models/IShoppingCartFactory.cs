using System.Web;
using System.Web.Mvc;

namespace Northwind.MusicStore.WebUI.Models
{
    public interface IShoppingCartFactory
    {
        ShoppingCart GetCart(HttpContextBase context);
        ShoppingCart GetCart(Controller controller);
    }
}