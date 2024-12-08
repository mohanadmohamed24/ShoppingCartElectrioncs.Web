using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.Entities.Rebositories
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRebsitory Category { get; }
        IProductRebsitory Product { get; }
        IApplicationUserRebsitory ApplicationUser { get; }
        IShoppingCartRebsitory ShoppingCart { get; } 
        IOrderHeaderRebsitory OrderHeader { get; }
        IOrderDetailRebsitory OrderDetail { get; }
        int Comblete();
    }
}
