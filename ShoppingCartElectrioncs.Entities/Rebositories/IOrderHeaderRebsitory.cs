﻿using ShoppingCartElectrioncs.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartElectrioncs.Entities.Rebositories
{
    public interface IOrderHeaderRebsitory : IGenericRebsitory<OrderHeader>
    {
        void Update(OrderHeader orderHeader); 
        void UpdateStatus(int id ,string? OrderStauts ,string? PaymentStauts);
    }
}