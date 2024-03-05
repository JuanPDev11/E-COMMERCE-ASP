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
    public class ApplicationUserRepo : Repository<ApplicationUser> ,IApplicationUser
    {

        private readonly ApplicationDbContext _context;
        public ApplicationUserRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ApplicationUser user)
        {
            _context.ApplicationUsers.Update(user);
            //var userDB = _context.ApplicationUsers.FirstOrDefault(x => x.Id == user.Id);
            //if (userDB != null) 
            //{
            //    userDB.UserName = user.UserName;
            //    userDB.Email = user.Email;
            //    userDB.Name = user.Name;
            //    userDB.PhoneNumber = user.PhoneNumber;
            //    userDB.Address = user.Address;
            //    userDB.City = user.City;
            //    userDB.State = user.State;
            //    userDB.PinCode = user.PinCode;
            //}
        }
    }
}
