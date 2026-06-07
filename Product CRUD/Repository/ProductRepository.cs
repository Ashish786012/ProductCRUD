// =============================================
// File: Repository/ProductRepository.cs
// Framework: ASP.NET Core
// Key differences from MVC 5:
//   - Connection string read via IConfiguration (injected),
//     NOT ConfigurationManager (that's .NET Framework only)
// =============================================

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductCRUD.Models;


namespace ProductCRUD.Repository
{
    public class ProductRepository
    {
        // In ASP.NET Core, connection strings come from appsettings.json
        // via IConfiguration — injected through the constructor
        private readonly string _connectionString;
        public ProductRepository(IConfiguration configuration)
        {
            _connectionString =
                configuration.GetConnectionString("DefaultConnection");
        }

        // -----------------------------------------------
        // GET ALL
        // -----------------------------------------------
        public List<Product> GetAll()
        {
            var products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Name, Price, Quantity FROM Products ORDER BY Id DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            products.Add(MapReaderToProduct(reader));
                    }
                }
            }

            return products;
        }

        // -----------------------------------------------
        // GET BY ID
        // -----------------------------------------------
        public Product GetById(int id)
        {
            Product product = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Name, Price, Quantity FROM Products WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            product = MapReaderToProduct(reader);
                    }
                }
            }

            return product;
        }

        // -----------------------------------------------
        // CREATE
        // -----------------------------------------------
        public void Create(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Products (Name, Price, Quantity)
                               VALUES (@Name, @Price, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name",     product.Name);
                    cmd.Parameters.AddWithValue("@Price",    product.Price);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // -----------------------------------------------
        // UPDATE
        // -----------------------------------------------
        public void Update(Product product)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Products
                               SET Name     = @Name,
                                   Price    = @Price,
                                   Quantity = @Quantity
                               WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id",       product.Id);
                    cmd.Parameters.AddWithValue("@Name",     product.Name);
                    cmd.Parameters.AddWithValue("@Price",    product.Price);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // -----------------------------------------------
        // DELETE
        // -----------------------------------------------
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Products WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // -----------------------------------------------
        // HELPER: Map a reader row to a Product object
        // -----------------------------------------------
        private Product MapReaderToProduct(SqlDataReader reader)
        {
            return new Product
            {
                Id       = Convert.ToInt32(reader["Id"]),
                Name     = reader["Name"].ToString(),
                Price    = Convert.ToDecimal(reader["Price"]),
                Quantity = Convert.ToInt32(reader["Quantity"])
            };
        }
    }
}
