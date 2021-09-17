namespace ProductService.Data
{
    using Microsoft.EntityFrameworkCore;
    using ProductService.Entities;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> products { get; set; }

    }
}
