using RepositorySample.Params;
using RepositorySample.Entities;
using RepositorySample.Repository.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RepositorySample.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IOrderStorage _storage;

        public OrderRepository(IOrderStorage storage)
        {
            _storage = storage;
        }

        public void AddItem(OrderItem item, int orderId, SqlConnection conn, SqlTransaction transaction)
        {
            _storage.AddItem(item, orderId, conn, transaction);
        }

        public void Create(CreateOrderParams Params)
        {
            var order = new Order
            {
                CustomerId = Params.CustomerId,
                OrderReference = Params.OrderReference,
                Items = Params.Items
            };

            _storage.Create(order);
        }

        public List<Order> Filter(FilterOrderCriteria? Criterias = null)
        {
            return _storage.Filter(Criterias).ToList();
        }

        public void GetById(int OrderId)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateOrderParams Params)
        {
            throw new NotImplementedException();
        }
    }
}
