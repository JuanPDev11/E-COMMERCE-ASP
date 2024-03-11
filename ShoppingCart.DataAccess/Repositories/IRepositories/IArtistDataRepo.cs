using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.Repositories.IRepositories
{
    public interface IArtistDataRepo : IRepository<ArtistData>
    {
        void Update(ArtistData artistData);
        void IncrementFollowers(int id);
        void DecrementFollowers(int id);
    }
}
