using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.ViewModels
{
    public class CartVM
    {
        
        public IEnumerable<Cart> Carts { get; set; } 

        public OrderHeader OrderHeader { get; set; }
    }
}

