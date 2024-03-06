using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Repositories.IRepositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        int IncrementCount(Cart cart, int count);
        int DecrementCount(Cart cart, int count);
    }
}
