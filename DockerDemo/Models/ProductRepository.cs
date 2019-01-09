using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerDemo.Models
{
    public class ProductRepository : IRepository
    {
        private ProductDbContext context;
        public ProductRepository(ProductDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;
    }
}
