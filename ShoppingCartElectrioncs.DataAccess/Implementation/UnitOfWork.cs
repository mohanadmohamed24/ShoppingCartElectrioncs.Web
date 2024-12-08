using ShoppingCartElectrioncs.Entities.Rebositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRebsitory Category { get; private set; }
        public IProductRebsitory Product { get; private set; }
        public IApplicationUserRebsitory ApplicationUser { get; private set; }

        public IShoppingCartRebsitory ShoppingCart { get; private set; }

        public IOrderHeaderRebsitory OrderHeader { get; private set; }

        public IOrderDetailRebsitory OrderDetail { get; private set; }


        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) 
        {
            _context = context;
            Category=new CategoryRebsitory(context);
            Product=new ProductRebsitory(context); 
            ApplicationUser=new ApplicationUserRebsitory(context);
            ShoppingCart = new ShoppingCartRebsitory(context); 
            OrderDetail=new OrderDetailRebsitory(context);
            OrderHeader=new OrderHeaderRebsitory(context); 
        }

        public int Comblete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
