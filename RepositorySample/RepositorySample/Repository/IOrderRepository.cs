using RepositorySample.Dtos;
using RepositorySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Repository
{
    public interface IOrderRepository
    {
        public void AddItem(OrderItem item);

        public List<Order> Filter();

        public void GetById(int OrderId);

        public void Update(UpdateOrderParams Params);

        public void Create(CreateOrderParams Params);

    }
}
