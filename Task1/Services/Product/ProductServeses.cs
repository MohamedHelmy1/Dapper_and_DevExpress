using Dapper;
using System.Data;
using Task1.DataModel;

namespace Task1.Services.Product
{
    public class ProductServeses : IProductServeses
    {
        private readonly IDbConnection db;

        public ProductServeses(IDbConnection db)
        {
            this.db = db;
        }
        public async Task<int> AddProduct(Products product)
        {
            var sqlQuery = "INSERT INTO Products (Name, Price) VALUES (@Name, @Price)";
            var result = await db.ExecuteAsync(sqlQuery, product);
            return result;
        }

        public async Task<int> DeleteProduct(int id)
        {
            int data = await db.ExecuteAsync("UPDATE Products SET IsDeleted = 1 WHERE Id = @Id", new { Id = id });
            return data;
        }

        public async Task<Products> GetProduct(int id)
        {
            var product = await db.QueryFirstOrDefaultAsync<Products>("SELECT * FROM Products WHERE Id = @Id  AND  IsDeleted <>1", new { Id = id });
            return product;
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            var products = await db.QueryAsync<Products>("SELECT * FROM Products where IsDeleted <>1");
            return products.ToList();
        }

        public async Task<int> UpdateProduct(Products product)
        {
            var data = await db.ExecuteAsync("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", product);
            return data;
        }
    }
}
