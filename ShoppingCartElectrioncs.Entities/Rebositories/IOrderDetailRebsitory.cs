using ShoppingCartElectrioncs.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.Entities.Rebositories
{
    public interface IOrderDetailRebsitory : IGenericRebsitory<OrderDetail>
    {
        void Update(OrderDetail orderDetail ); 
    }
}
