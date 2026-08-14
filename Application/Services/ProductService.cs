using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Product;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class ProductService : IProductService
    {
    
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            IProductRepository productRepository,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }


        public async Task<List<ProductDto>> GetAllAsync()
        {
            _logger.LogInformation("Fetching all products.");

            var products = await _productRepository.GetAllAsync();

            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return MapToDto(product);
        }


        public async Task<ProductDto> CreateAsync(CreateProductDto request)

        {
            _logger.LogInformation(
                "Creating product with name {ProductName}",
                request.ProductName);

            var product = new Product
            {
                ProductName = request.ProductName,
                CreatedBy = "System",
                CreatedOn = DateTime.UtcNow
            };

            var createdProduct = await _productRepository.CreateAsync(product);

            return MapToDto(createdProduct);
        }

        public async Task<ProductDto?> UpdateAsync(
            int id,
            UpdateProductDto request)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            product.ProductName = request.ProductName;
            product.ModifiedBy = "System";
            product.ModifiedOn = DateTime.UtcNow;

            var updatedProduct =
                await _productRepository.UpdateAsync(product);

            return MapToDto(updatedProduct);
        }

    
        public async Task<bool> DeleteAsync(int id)
        {
            _logger.LogInformation(
                "Deleting product with Id {ProductId}",
                id);

            return await _productRepository.DeleteAsync(id);
        }

        private static ProductDto MapToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                CreatedBy = product.CreatedBy,
                CreatedOn = product.CreatedOn,
                ModifiedBy = product.ModifiedBy,
                ModifiedOn = product.ModifiedOn
            };
        }
    }

}

