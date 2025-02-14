using Task1.DataModel;

namespace Task1.Services.Product
{
    public interface IProductServeses
    {
        Task<IEnumerable<Products>> GetProducts();
        Task<Products> GetProduct(int id);
        Task<int> AddProduct(Products product);
        Task<int> DeleteProduct(int id);
        Task<int> UpdateProduct(Products product);

    }
}
