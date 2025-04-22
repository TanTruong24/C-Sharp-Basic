using RepositorySample.Params;
using RepositorySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace RepositorySample.Repository
{
    public interface IOrderRepository
    {
        public void AddItem(OrderItem item, int orderId, SqlConnection conn, SqlTransaction transaction);

        public List<Order> Filter(FilterOrderCriteria? Criterias = null);

        public void GetById(int OrderId);

        public void Update(UpdateOrderParams Params);

        public void Create(CreateOrderParams Params);

    }
}
