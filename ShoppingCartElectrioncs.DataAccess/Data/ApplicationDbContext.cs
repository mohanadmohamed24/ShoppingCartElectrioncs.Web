using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCartElectrioncs.Entities.Models;

namespace ShoppingCartElectrioncs.DataAccess
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options) 
        {
            
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products  { get; set; } 
        public DbSet<ApplicationUser> applicationUsers { get; set; } 
        public DbSet<ShoppingCart> shoppingCarts { get; set; }  
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
