﻿using ShoppingCartElectrioncs.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.Entities.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart>CartList { get; set; } 
        public OrderHeader OrderHeader { get; set; }

    }
}
