using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.Entities.Rebositories
{
    public interface IGenericRebsitory <T> where T:class 
    {
        //_context.categories.Include("products").ToList();
        //_context.categories.Include.Where(x=>x.Id==Id).ToList();
        IEnumerable<T> GetAll(Expression <Func<T, bool>>? predicate=null, string? IncludeWord = null );
        //_context.categories.Include("products").ToList();
        //_context.categories.Include.Where(x=>x.Id==Id).ToList();
        T GetFirstOrDefault(Expression<Func<T, bool>>? predicate=null, string? IncludeWord = null);
        //_context.categories.Add(category)
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

    }
}
