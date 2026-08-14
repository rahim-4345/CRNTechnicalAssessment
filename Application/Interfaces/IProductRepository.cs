using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> CreateAsync(Product product);

        Task<Product> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.Entities;

//namespace Application.Interfaces
//{
//    public class IProductRepository
//    {

//            Task<List<Product>> GetAllAsync();

//            Task<Product?> GetByIdAsync(int id);

//            Task<Product> CreateAsync(Product product);

//            Task<Product> UpdateAsync(Product product);

//            Task<bool> DeleteAsync(int id);

//    }
//}
