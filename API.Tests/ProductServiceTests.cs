using Moq;
using Application.Interfaces;
using Application.Services;
using Application.DTOs.Product;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace API.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<ILogger<ProductService>> _loggerMock;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();

            _loggerMock = new Mock<ILogger<ProductService>>();

            _productService =
                new ProductService(
                    _repositoryMock.Object,
                    _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProducts()
        {
            var products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    ProductName = "Samsung"
                },
                new Product
                {
                    Id = 2,
                    ProductName = "Nokia"
                }
            };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(products);

            var result =
                await _productService.GetAllAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.Equal(
                "Samsung",
                result[0].ProductName);

            Assert.Equal(
                "Nokia",
                result[1].ProductName);

            _repositoryMock.Verify(
                x => x.GetAllAsync(),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct()
        {
            var product = new Product
            {
                Id = 1,
                ProductName = "Samsung"
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            var result =
                await _productService.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal(
                "Samsung",
                result.ProductName);

            _repositoryMock.Verify(
                x => x.GetByIdAsync(1),
                Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenProductNotFound()
        {
            _repositoryMock
                .Setup(x => x.GetByIdAsync(999))
                .ReturnsAsync((Product?)null);

            var result =
                await _productService.GetByIdAsync(999);

            Assert.Null(result);

            _repositoryMock.Verify(
                x => x.GetByIdAsync(999),
                Times.Once);
        }
    }
}