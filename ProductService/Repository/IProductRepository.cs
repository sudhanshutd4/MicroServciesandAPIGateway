namespace ProductService.Repository
{
    using global::ProductService.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProductById(int id);

        Task<Product> GetProductByName(string productname);

        Task<Product> InsertProduct(Product product);

        void DeleteProduct(int id);

        void Save();

    }
}
