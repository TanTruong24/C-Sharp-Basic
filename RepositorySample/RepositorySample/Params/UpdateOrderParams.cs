using RepositorySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Params
{
    public class UpdateOrderParams
    {
        public required string OrderReference { get; set; }
        public List<OrderItem> Items { get; set; } = [];

        public UpdateOrderParams(string orderReference, List<OrderItem> items)
        {
            OrderReference = orderReference;
            Items = items;
        }

        
    }
}
