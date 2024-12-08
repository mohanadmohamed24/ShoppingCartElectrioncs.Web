using ShoppingCartElectrioncs.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.Entities.Rebositories
{
    public interface IShoppingCartRebsitory : IGenericRebsitory<ShoppingCart>
    {
        int IncraseCount(ShoppingCart shoppingCart ,int count ); 
        int DecraseCount(ShoppingCart shoppingCart ,int count ); 
    }
}
