using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Text.Json;

namespace API.Tests
{
    public class ProductsControllerTests
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ProductsControllerTests()
        {
            _factory =
                new WebApplicationFactory<Program>();

            _client =
                _factory.CreateClient();
        }

        [Fact]
        public async Task GetProducts_ShouldReturnSuccess()
        {
            var response =
                await _client.GetAsync("/api/Products");

            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);
        }

        [Fact]
        public async Task CreateProduct_WithoutToken_ShouldReturnUnauthorized()
        {
            var json = """
    {
        "productName": "Test Product"
    }
    """;

            var content =
                new StringContent(
                    json,
                    System.Text.Encoding.UTF8,
                    "application/json");

            var response =
                await _client.PostAsync(
                    "/api/Products",
                    content);

            Assert.Equal(
                HttpStatusCode.Unauthorized,
                response.StatusCode);
        }

        private async Task<string> GetAccessTokenAsync()
        {
            var loginRequest = new
            {
                username = "rahim",
                password = "Password@123"
            };

            var json = JsonSerializer.Serialize(loginRequest);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _client.PostAsync(
                "/api/Auth/login",
                content);

            response.EnsureSuccessStatusCode();

            var responseBody =
                await response.Content.ReadAsStringAsync();

            using var document =
                JsonDocument.Parse(responseBody);

            return document
                .RootElement
                .GetProperty("accessToken")
                .GetString()!;
        }

        [Fact]
        public async Task CreateProduct_WithValidToken_ShouldReturnCreated()
        {
            var token = await GetAccessTokenAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);

            var request = new
            {
                productName = "Integration Test Product"
            };

            var json =
                JsonSerializer.Serialize(request);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response =
                await _client.PostAsync(
                    "/api/Products",
                    content);

            Assert.Equal(
                HttpStatusCode.Created,
                response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_WithValidToken_ShouldReturnSuccess()
        {

            var token = await GetAccessTokenAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);

            int productId = 2;

            var request = new
            {
                productName = "Updated Samsung"
            };

            var json =
                JsonSerializer.Serialize(request);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");


            var response =
                await _client.PutAsync(
                    $"/api/Products/{productId}",
                    content);


            Assert.Equal(
                HttpStatusCode.OK,
                response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_WithValidToken_ShouldReturnNoContent()
        {

            var token = await GetAccessTokenAsync();

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);

            var createRequest = new
            {
                productName = "Product For Delete Test"
            };

            var createJson =
                JsonSerializer.Serialize(createRequest);

            var createContent =
                new StringContent(
                    createJson,
                    Encoding.UTF8,
                    "application/json");

            var createResponse =
                await _client.PostAsync(
                    "/api/Products",
                    createContent);

            Assert.Equal(
                HttpStatusCode.Created,
                createResponse.StatusCode);

            var responseBody =
                await createResponse.Content.ReadAsStringAsync();

            using var document =
                JsonDocument.Parse(responseBody);

            var productId =
                document.RootElement
                    .GetProperty("id")
                    .GetInt32();

            var deleteResponse =
                await _client.DeleteAsync(
                    $"/api/Products/{productId}");

            Assert.Equal(
                HttpStatusCode.NoContent,
                deleteResponse.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_WithoutToken_ShouldReturnUnauthorized()
        {
            var productId = 2;

            _client.DefaultRequestHeaders.Authorization = null;

            var response =
                await _client.DeleteAsync(
                    $"/api/Products/{productId}");

            Assert.Equal(
                HttpStatusCode.Unauthorized,
                response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_WithoutToken_ShouldReturnUnauthorized()
        {
            var productId = 2;

            _client.DefaultRequestHeaders.Authorization = null;

            var request = new
            {
                productName = "Unauthorized Update"
            };

            var json =
                JsonSerializer.Serialize(request);

            var content =
                new StringContent(
                    json,
                    Encoding.UTF8,
                    "application/json");

            var response =
                await _client.PutAsync(
                    $"/api/Products/{productId}",
                    content);

            Assert.Equal(
                HttpStatusCode.Unauthorized,
                response.StatusCode);
        }
    }
}
