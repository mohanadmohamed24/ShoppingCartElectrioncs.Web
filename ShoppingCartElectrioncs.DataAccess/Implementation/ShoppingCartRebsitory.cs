using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.DataAccess.Implementation
{
    public class ShoppingCartRebsitory : GenericRebsitory<ShoppingCart>, IShoppingCartRebsitory
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRebsitory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int DecraseCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        public int IncraseCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }
    }
}
