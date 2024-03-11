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
    public class ArtistDataRepo : Repository<ArtistData> , IArtistDataRepo
    {
        private readonly ApplicationDbContext _context;

        public ArtistDataRepo(ApplicationDbContext context) :base(context)
        {
            _context = context;
        }


        public void Update(ArtistData artist)
        {
            var artistDB = _context.Artists.FirstOrDefault(x=>x.ID == artist.ID);
            if (artistDB != null) 
            {
                artistDB.Name = artist.Name;
                artistDB.Description = artist.Description;
                artistDB.Age = artist.Age;
                artistDB.Volume = artist.Volume;
                artistDB.Followers = artist.Followers;
                artistDB.TotalSold = artist.TotalSold;
                if(artistDB.ImageUrl != null) 
                {
                    artistDB.ImageUrl = artist.ImageUrl;
                }
            }
        }

        public void DecrementFollowers(int id)
        {
            var artistDB = _context.Artists.FirstOrDefault(x=>x.ID == id);
            if (artistDB != null)
            {
                artistDB.Followers -= 1;
            }
        }

        public void IncrementFollowers(int id)
        {
            var artistDB = _context.Artists.FirstOrDefault(x => x.ID == id);
            if (artistDB != null)
            {
                artistDB.Followers += 1;
            }
        }
    }
}
