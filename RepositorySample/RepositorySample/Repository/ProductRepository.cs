using Microsoft.Data.SqlClient;
using RepositorySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void Create(Product product)
        {
            throw new NotImplementedException();
        }

        public void Delete(int productId)
        {
            throw new NotImplementedException();
        }

        public Array Filter()
        {
            throw new NotImplementedException();
        }

        public decimal GetPriceById(int productId, SqlConnection conn, SqlTransaction transaction)
        {
            var cmd = new SqlCommand("SELECT price FROM products WHERE id = @productId", conn, transaction);
            cmd.Parameters.AddWithValue("@productId", productId);

            var result = cmd.ExecuteScalar();

            if (result == null || result == DBNull.Value)
            {
                throw new Exception($"Not found Price for Product ID ={productId}");
            }

            return Convert.ToDecimal(result);
        }

        public void ReduceQuantity(int productId, int quantityToReduce, SqlConnection conn, SqlTransaction transaction)
        {
            var cmd = new SqlCommand("SELECT remain_quantity FROM products  WHERE id = @productId", conn, transaction);
            cmd.Parameters.AddWithValue("@productId", productId);

            var currentQtyObj = cmd.ExecuteScalar();

            if (currentQtyObj == null || currentQtyObj == DBNull.Value)
            {
                throw new Exception($"Not found Renain Quantity for Product ID ={productId}");
            }

            int currenQuantity = Convert.ToInt32(currentQtyObj);

            if (currenQuantity < quantityToReduce)
            {
                throw new Exception($"Insufficient stock for product ID = {productId}. Available: {currenQuantity}");
            }

            var updateCmd = new SqlCommand("UPDATE products " +
                "SET remain_quantity = remain_quantity - @quantityToReduce " +
                "WHERE id = @productId", conn, transaction);

            updateCmd.Parameters.AddWithValue("@productId", productId);
            updateCmd.Parameters.AddWithValue("@quantityToReduce", quantityToReduce);
            updateCmd.ExecuteNonQuery();
        }

        public void Update(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
