namespace ProductService
{
    using Microsoft.EntityFrameworkCore;
    using ProductService.Data;
    using ProductService.Entities;
    using ProductService.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext dataContext)
        {
            _context = dataContext;
        }        

        public async Task<Product> GetProductById(int id)
        {
            return await _context.products.FindAsync(id);
        }

        public async Task<Product> GetProductByName(string productname)
        {
            return await _context.products.FindAsync(productname);
        }

        public async Task <IEnumerable<Product>> GetProducts()
        {
            return await _context.products.ToListAsync();            
        }

        public async Task<Product> InsertProduct(Product product)
        {
            var result = await _context.AddAsync(product);
            Save();
            return result.Entity;
        }

        public async void DeleteProduct(int id)
        {
            var result = await _context.products.FirstOrDefaultAsync(x => x.Id == id);
            if(result != null)
            {
                _context.products.Remove(result);
                Save();
            }
        }

        public async void Save()
        {
            await _context.SaveChangesAsync();
        }

    }
}
