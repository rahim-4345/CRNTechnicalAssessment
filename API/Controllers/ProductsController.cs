using Application.DTOs;
using Application.DTOs.Product;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateProductDto request)
        {
            var product = await _productService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                product);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(
            int id,
            UpdateProductDto request)
        {
            var product = await _productService.UpdateAsync(id, request);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}

//using Application.DTOs;
//using Application.DTOs.Product;
//using Application.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ProductsController : ControllerBase
//    {
//        private readonly IProductService _productService;

//        public ProductsController(IProductService productService)
//        {
//            _productService = productService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var products = await _productService.GetAllAsync();

//            return Ok(products);
//        }

//        [HttpGet("{id:int}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var product = await _productService.GetByIdAsync(id);

//            if (product == null)
//                return NotFound();

//            return Ok(product);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(CreateProductDto request)
//        {
//            var product = await _productService.CreateAsync(request);

//            return CreatedAtAction(
//                nameof(GetById),
//                new { id = product.Id },
//                product);
//        }

//        [HttpPut("{id:int}")]
//        public async Task<IActionResult> Update(
//            int id,
//            UpdateProductDto request)
//        {
//            var product = await _productService.UpdateAsync(id, request);

//            if (product == null)
//                return NotFound();

//            return Ok(product);
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var result = await _productService.DeleteAsync(id);

//            if (!result)
//                return NotFound();

//            return NoContent();
//        }
//    }
//}


//using Application.DTOs;
//using Application.DTOs.Product;
//using Application.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace API.Controllers
//{
//    [ApiController]
//    [Route("api/v1/products")]
//    public class ProductsController : ControllerBase
//    {
//        private readonly IProductService _productService;

//        public ProductsController(IProductService productService)
//        {
//            _productService = productService;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var products = await _productService.GetAllAsync();

//            return Ok(products);
//        }

//        [HttpGet("{id:int}")]
//        public async Task<IActionResult> GetById(int id)
//        {
//            var product = await _productService.GetByIdAsync(id);

//            if (product == null)
//            {
//                return NotFound(new
//                {
//                    message = "Product not found"
//                });
//            }

//            return Ok(product);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create(
//            CreateProductDto request)
//        {
//            var product =
//                await _productService.CreateAsync(request);

//            return CreatedAtAction(
//                nameof(GetById),
//                new { id = product.Id },
//                product);
//        }

//        [HttpPut("{id:int}")]
//        public async Task<IActionResult> Update(
//            int id,
//            UpdateProductDto request)
//        {
//            var product =
//                await _productService.UpdateAsync(id, request);

//            if (product == null)
//            {
//                return NotFound(new
//                {
//                    message = "Product not found"
//                });
//            }

//            return Ok(product);
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<IActionResult> Delete(int id)
//        {
//            var deleted =
//                await _productService.DeleteAsync(id);

//            if (!deleted)
//            {
//                return NotFound(new
//                {
//                    message = "Product not found"
//                });
//            }

//            return NoContent();
//        }
//    }
//}