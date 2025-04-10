using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace HR
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json", optional: false);

            IConfiguration configuration = builder.Build();

            using var conn = new SqlConnection(configuration.GetConnectionString("HRDB"));

            conn.Open();

            var trans = conn.BeginTransaction();

            //ListCountries(conn);
            //DisplayCountryCount(conn);

            //var cmd = conn.CreateCommand();
            //cmd.CommandText = "SELECT * FROM countries";

            CreateEmployee(
                "Truong",
                "TRANS 1",
                "aaa@gmail.com",
                "1234567",
                DateTime.Today,
                9,
                10000,
                6,
                100,
                conn, trans
                );

            //trans.Commit();

            trans.Rollback();

            conn.Close();
        }

        private static void DisplayCountryCount(SqlConnection conn)
        {
            var cmd = new SqlCommand("SELECT COUNT(*) FROM countries", conn);
            int c = (int)cmd.ExecuteScalar();
            Console.WriteLine($"Total: {c}");
        }

        private static void ListCountries(SqlConnection conn)
        {
            var cmd = new SqlCommand("SELECT * FROM countries", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader[0]}, Name: {reader.GetSqlString(1)}");
            }
            reader.Close();
        }

        private static int CreateEmployee(
            string firstName, 
            string lastName, 
            string email,
            string phone,
            DateTime hireDate, int jobId, double salary, int departmentId, int managerId,
            SqlConnection conn
, SqlTransaction trans)
        {
            var cmd = new SqlCommand(@"INSERT INTO employees (first_name, last_name, email, phone_number, hire_date, job_id, salary, manager_id, department_id)
                    VALUES  (@first_name, @last_name, @email, @phone_number, @hire_date, @job_id, @salary, @manager_id, @department_id)", conn, trans);

            cmd.Parameters.Add(new SqlParameter(@"first_name", System.Data.SqlDbType.VarChar, 20)).Value = firstName;
            cmd.Parameters.Add(new SqlParameter(@"last_name", System.Data.SqlDbType.VarChar, 25)).Value = lastName;
            cmd.Parameters.Add(new SqlParameter(@"email", System.Data.SqlDbType.VarChar, 100)).Value = email;
            cmd.Parameters.Add(new SqlParameter(@"phone_number", System.Data.SqlDbType.VarChar, 20)).Value = phone;
            cmd.Parameters.Add(new SqlParameter(@"hire_date", System.Data.SqlDbType.Date)).Value = hireDate;
            cmd.Parameters.Add(new SqlParameter(@"job_id", System.Data.SqlDbType.Int)).Value = jobId;
            cmd.Parameters.Add(new SqlParameter(@"salary", System.Data.SqlDbType.Decimal)).Value = salary;
            cmd.Parameters.Add(new SqlParameter(@"manager_id", System.Data.SqlDbType.Int)).Value = managerId;
            cmd.Parameters.Add(new SqlParameter(@"department_id", System.Data.SqlDbType.Int)).Value = departmentId;

            var result = cmd.ExecuteNonQuery();

            return result;
        }
    }
}
