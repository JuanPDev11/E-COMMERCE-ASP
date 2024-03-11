using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.DataAccess.ViewModels
{
    public class ArtistVM
    {
        public ArtistData ArtistData { get; set; }
        [ValidateNever]
        public IEnumerable<ArtistData> Artists { get; set; } = new List<ArtistData>();
        [ValidateNever]
        public IEnumerable<Product> Products { get; set; } = new List<Product>();  

    }
}
