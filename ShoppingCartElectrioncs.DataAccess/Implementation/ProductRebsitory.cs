using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.DataAccess.Implementation
{
    public class ProductRebsitory : GenericRebsitory<Product>,IProductRebsitory
    {
        private readonly ApplicationDbContext _context;
        public ProductRebsitory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product Product)
        {
            var ProductInDb=_context.Products.FirstOrDefault(x=> x.Id == Product.Id);
            if (ProductInDb != null)
            {
                ProductInDb.Name = ProductInDb.Name;
                ProductInDb.Description = Product.Description;
                ProductInDb.Price = Product.Price;
                ProductInDb.Img = Product.Img;
                ProductInDb.CategoryId= Product.CategoryId;
            }
        }
    }
}
