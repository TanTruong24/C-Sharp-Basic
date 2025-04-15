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

        public void GetAll();

        public void GetById(Guid OrderId);

        public void Update(UpdateOrderParams Params);

        public void Create(CreateOrderParams Params);

    }
}
