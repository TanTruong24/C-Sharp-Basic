using Microsoft.Data.SqlClient;

namespace ADOAppSample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            TestDatabaseConnection();
        }

        public static void TestDatabaseConnection()
        {

            var conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=HR;Trusted_Connection=yes; TrustServerCertificate=True");
            try
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT employee_id, email FROM employees";

                // sử dụng using tự động đóng, khi thoát khỏi ngoặc {} của using var reader
                using var reader = cmd.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader.GetInt32(0)}, Name: {reader.GetString(1)}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
    }
}
