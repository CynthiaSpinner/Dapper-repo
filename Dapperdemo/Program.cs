using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net.WebSockets;

namespace Dapperdemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            var instance = new DapperDepartmentRepository(conn);

            instance.InsertDepartment("Women's Clothing");

            var collectionOfDept = instance.GetAllDepartments();

            foreach (var department in collectionOfDept )
            {
                Console.WriteLine(department.DepartmentID);
                Console.WriteLine(department.Name);
                Console.WriteLine();
            }

        }
    }
}
