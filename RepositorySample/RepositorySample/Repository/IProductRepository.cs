using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using RepositorySample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorySample.Repository
{
    public interface IProductRepository
    {
        public void Create(Product product);

        public void Update(int productId);

        public void Delete(int productId);

        public Array Filter();

        public void ReduceQuantity(int productId, int quantityToReduct, SqlConnection conn, SqlTransaction transaction);

        public decimal GetPriceById(int productId, SqlConnection conn, SqlTransaction transaction);
    }
}
