using System;
using System.Data.SqlClient;

namespace CGHClientServer1.DB
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine($"Opening database connection....");

            try
            {
                Console.WriteLine($"Using SqlConnection....");
                // SqlConnection.CreateFile("CountryStateCity");
                SqlConnection dbConnectionSql = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CountryStateCity;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
                dbConnectionSql.Open();

                SampleData_SQLServer.LoadData(dbConnectionSql);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred {ex.Message} at {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine($"Done!");
            }
        }
    }
}