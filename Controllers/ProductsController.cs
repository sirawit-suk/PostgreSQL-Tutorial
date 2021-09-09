using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.DTOs;
using ProductsApi.Models;
using ProductsApi.Repositories;

namespace ProductsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.Get(id);
            if(product == null)
            {
                return NotFound();
            }
                
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            var product = new Product // alt: Product product = new(){}
            {
                Name = createProductDTO.Name,
                Price = createProductDTO.Price,
                DateCreated = DateTime.Now
            };

            await _productRepository.Add(product);
            return CreatedAtAction(nameof(GetProduct),new {id = product.ProductId}, product); //need to change to 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
        {
            Product product = new()
            {
                ProductId = id,
                Name = updateProductDTO.Name,
                Price = updateProductDTO.Price
            };

            await _productRepository.Update(product);
            return NoContent();;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            await _productRepository.Delete(id);
            return NoContent();
        }

    }
}