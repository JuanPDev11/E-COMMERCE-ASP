﻿using ShoppingCart.DataAccess.Data;
using ShoppingCart.DataAccess.Repositories.IRepositories;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Repositories
{
    public class CartRepository : Repository<Cart> , ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Cart cart)
        {
            _context.Carts.Update(cart);
            //var cartDB = _context.Carts.FirstOrDefault(x=> x.Id == cart.Id);
            //if (cartDB != null) 
            //{
            //    cartDB.Product = cart.Product;
            //    cartDB.ApplicationUserId = cart.ApplicationUserId;
            //    cartDB.Count = cart.Count;
            //    cartDB.ProductID = cart.ProductID;
            //}
        }
    }
}
