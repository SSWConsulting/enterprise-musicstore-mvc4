using System.Collections.Generic;
using Northwind.MusicStore.Domain;

namespace Northwind.MusicStore.WebUI.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}