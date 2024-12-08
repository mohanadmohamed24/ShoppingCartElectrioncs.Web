using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.DataAccess.Implementation
{
    public class OrderHeaderRebsitory : GenericRebsitory<OrderHeader>,IOrderHeaderRebsitory
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderRebsitory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderHeader orderHeader)
        {

            _context.OrderHeaders.Update(orderHeader); 
        }

        public void UpdateStatus(int id, string OrderStauts, string PaymentStauts)
        { 
            var orderFromDB=_context.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (orderFromDB != null)
            {
                orderFromDB.OrderStatus = OrderStauts;
                orderFromDB.PaymentDate = DateTime.Now;
                if (PaymentStauts != null)
                {
                    orderFromDB.PaymentStatus = PaymentStauts;
                }
            }
        }
    }
}
