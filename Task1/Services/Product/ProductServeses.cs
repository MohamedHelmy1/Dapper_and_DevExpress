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

        public Task<int> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Products> GetProduct(int id)
        {
            var product = await db.QueryFirstOrDefaultAsync<Products>("SELECT * FROM Products WHERE Id = @Id", new { Id = id });
            return product;
        }

        public async Task<IEnumerable<Products>> GetProducts()
        {
            var products = await db.QueryAsync<Products>("SELECT * FROM Products");
            return products.ToList();
        }

        public Task<int> UpdateProduct(Products product)
        {
            throw new NotImplementedException();
        }
    }
}
