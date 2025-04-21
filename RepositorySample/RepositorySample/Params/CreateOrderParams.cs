using RepositorySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Params
{
    public class CreateOrderParams
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public required string OrderReference { get; set; }

        public List<OrderItem> Items { get; set; } = [];
    }
}
