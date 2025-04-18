using Microsoft.Data.SqlClient;
using RepositorySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Repository.SqlServer
{
    public interface IOrderStorage
    {
        IEnumerable<Order> Filter();

        void Create(Order order);

        void AddItems(List<OrderItem> items, int orderId, SqlConnection conn, SqlTransaction transaction);
    }
}
