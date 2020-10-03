using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Model
{
    public interface IProductRepository 
    {
        Product GetProduct(int Id);

        IEnumerable<Product> GetAllProduct();

        Product Add(Product product);

        Product Update(Product productChanges);

        Product Delete(int id);
    }
}
