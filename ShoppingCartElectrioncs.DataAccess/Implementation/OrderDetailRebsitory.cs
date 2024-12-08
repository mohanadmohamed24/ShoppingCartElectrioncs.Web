using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.DataAccess.Implementation
{
    public class OrderDetailRebsitory : GenericRebsitory<OrderDetail>, IOrderDetailRebsitory
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRebsitory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update( orderDetail);
          
        }
    }
}
