using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.DataAccess.Implementation
{
    public class ApplicationUserRebsitory : GenericRebsitory<ApplicationUser>, IApplicationUserRebsitory
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRebsitory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
