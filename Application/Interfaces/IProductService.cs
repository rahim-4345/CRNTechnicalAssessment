using Application.DTOs;
using Application.DTOs.Product;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();

        Task<ProductDto?> GetByIdAsync(int id);

        Task<ProductDto> CreateAsync(CreateProductDto request);

        Task<ProductDto?> UpdateAsync(
            int id,
            UpdateProductDto request);

        Task<bool> DeleteAsync(int id);
    }
}


//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Application.DTOs.Product;
//using Application.DTOs;

//namespace Application.Interfaces
//{
//    public class IProductService
//    {
//        Task<List<ProductDto>> GetAllAsync();

//        Task<ProductDto?> GetByIdAsync(int id);

//        Task<ProductDto> CreateAsync(CreateProductDto request);

//        Task<ProductDto?> UpdateAsync(int id, UpdateProductDto request);

//        Task<bool> DeleteAsync(int id);
//    }
//}
