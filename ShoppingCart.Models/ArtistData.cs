using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class ArtistData
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [ValidateNever]
        public string ImageUrl { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required]
        public int Age { get; set; }

        public int Volume { get; set; }
        public int TotalSold { get; set; }
        public int Followers { get; set; }

        
    }
}
