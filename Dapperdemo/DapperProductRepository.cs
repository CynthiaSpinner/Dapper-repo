using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapperdemo
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute("INSERT INTO products (Name, price, categoryID) " +
                          "VALUES (@name, @price, @categoryID);",
                new { name = name, price = price, categoryID = categoryID }); 
           
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products;");
        }

        public void UpdateProduct(int productID, string nameUpdated)
        {
            _conn.Execute("UPDATE products SET Name = @nameUpdated WHERE ProductID = @productID;",
                new { productID = productID, nameUpdated = nameUpdated });
        }
        public void DeleteProduct(int productID) 
        {
            _conn.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
            _conn.Execute("DELETE FROM sales WHERE ProductID = @productID;",
                new { productID = productID });
            _conn.Execute("DELETE FROM products WHERE ProductID = @productID;",
                new { productID = productID });
        }
    }
}
