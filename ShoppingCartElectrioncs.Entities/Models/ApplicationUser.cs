using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.Entities.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required] 
        public string Name { get; set; }
        public string Address { get; set; } 
        public string City { get; set; } 
    }
}
