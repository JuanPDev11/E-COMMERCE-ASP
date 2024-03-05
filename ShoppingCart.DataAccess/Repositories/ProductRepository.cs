using ShoppingCart.DataAccess.Data;
using ShoppingCart.DataAccess.Repositories.IRepositories;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Repositories
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productDB = _context.products.FirstOrDefault(x => x.Id == product.Id);
            if (productDB != null)
            {
                productDB.Name = product.Name;
                productDB.Description = product.Description;
                productDB.Price = product.Price;
                if(productDB.ImageUrl != null)
                {
                    productDB.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
