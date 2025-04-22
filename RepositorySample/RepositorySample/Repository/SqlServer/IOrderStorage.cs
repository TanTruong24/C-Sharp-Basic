using Microsoft.Data.SqlClient;
using RepositorySample.Entities;
using RepositorySample.Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Repository.SqlServer
{
    public interface IOrderStorage
    {
        IEnumerable<Order> Filter(FilterOrderCriteria? Criterias);

        void Create(Order order);

        void AddItem(OrderItem item, int orderId, SqlConnection conn, SqlTransaction transaction);
    }
}
