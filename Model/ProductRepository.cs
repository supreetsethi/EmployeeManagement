using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public Product Add(Product product)
        {
            appDbContext.Product.Add(product); ;
            appDbContext.SaveChanges();
            return product;
        }

        public Product Delete(int id)
        {

            Product product = appDbContext.Product.Find(id);
            if (product != null)
            {
                appDbContext.Product.Remove(product);
                appDbContext.SaveChanges();
            }
            return product;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return appDbContext.Product;
        }

        public Product GetProduct(int Id)
        {
            return appDbContext.Product.Find(Id);
        }

        public Product Update(Product productChanges)
        {
            var product = appDbContext.Product.Attach(productChanges);
            product.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            appDbContext.SaveChanges();
            return productChanges;
        }
    }
}
