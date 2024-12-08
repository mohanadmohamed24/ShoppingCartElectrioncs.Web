using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.Entities.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int productId { get; set; }

        [ForeignKey("productId")]
        [ValidateNever]
        public Product product { get; set; } 

        [Range(1, 100, ErrorMessage = "you must enter value between 1 to 100")]
        public int Count { get; set; }

        public string applicationUserId { get; set; }
        [ForeignKey("applicationUserId")]
        [ValidateNever]
        public ApplicationUser applicationUser { get; set; }

    }
}
