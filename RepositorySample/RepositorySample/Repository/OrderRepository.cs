using RepositorySample.Dtos;
using RepositorySample.Entities;
using RepositorySample.Repository.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Repository
{
    class OrderRepository : IOrderRepository
    {
        private readonly IOrderStorage _storage;

        public OrderRepository(IOrderStorage storage)
        {
            _storage = storage;
        }

        public void AddItem(OrderItem item)
        {
            throw new NotImplementedException();
        }

        public void Create(CreateOrderParams Params)
        {
            throw new NotImplementedException();
        }

        public List<Order> Filter()
        {
            return _storage.Filter().ToList();
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
