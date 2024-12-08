using ShoppingCartElectrioncs.Entities.Models;
using ShoppingCartElectrioncs.Entities.Rebositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.DataAccess.Implementation
{
    public class CategoryRebsitory : GenericRebsitory<Category>,ICategoryRebsitory
    {
        private readonly ApplicationDbContext _context;
        public CategoryRebsitory(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var CategoryInDb=_context.Categories.FirstOrDefault(x=> x.Id == category.Id);
            if (CategoryInDb != null)
            {
                CategoryInDb.Name = CategoryInDb.Name;
                CategoryInDb.Description = category.Description;
                CategoryInDb.CreatedTime = DateTime.Now;

            }
        }
    }
}
