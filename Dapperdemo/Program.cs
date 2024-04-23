using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;


namespace Dapperdemo
{
    internal class Program
    {
        //gets the connection string from the appsettings.json // 
        static IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

        static string connString = config.GetConnectionString("DefaultConnection");


        // made my IDbConnection static in the program to make the instance standard through the program
        static IDbConnection conn = new MySqlConnection(connString);


        //made static for the same reason above.
        static DapperProductRepository instance1 = new DapperProductRepository(conn);
        static void Main(string[] args)
        {
            

            

            #region Department Section

            //var instance = new DapperDepartmentRepository(conn);

            //instance.InsertDepartment("Women's Clothing");

            //var collectionOfDept = instance.GetAllDepartments();

            //foreach (var department in collectionOfDept )
            //{
            //Console.WriteLine(department.DepartmentID);
            //Console.WriteLine(department.Name);
            //Console.WriteLine();
            //}
            #endregion

            
            var products = instance1.GetAllProducts();

            instance1.CreateProduct("Tang-top", 10.99, 8); // need to create new category in order to give a 
            // category number that doesnt already exist. 

            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name}, {product.Price}, {product.CategoryID}, {product.ProductID}");
            }

            DeleteProduct();

            instance1.GetAllProducts();
           
        }

        static void DeleteProduct()
        {
            

            Console.WriteLine("input the productID of the product you wish to delete...");

            var userProductID = int.Parse(Console.ReadLine());

            instance1.DeleteProduct(userProductID);
        }

        static void UpdateProductName()
        {
            Console.WriteLine("input the productID of the product you wish to update..");

            var userProductID = int.Parse(Console.ReadLine());

            Console.WriteLine($"enter the new name of {userProductID}...");
            var userProductName = Console.ReadLine();

            instance1.UpdateProduct(userProductID, userProductName);
        }

          
    }
}


//TODO's
//ask user what they would like to do... 
// after a task, ask if there are more they would like to do with in that method, if not, bring back to main
// have information loop, 
// I want to delete all the extra departments, and products,
// do the same for categories. 