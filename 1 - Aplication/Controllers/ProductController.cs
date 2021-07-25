using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopWebAPI.Aplication.Models;
using ShopWebAPI.Domain.Entities;
using ShopWebAPI.Infra.Repositories;
using System.Collections.Generic;

namespace ShopWebAPI.Aplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IRepository<Product> _repository;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IRepository<Product> repository, ILogger<ProductController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string productId)
        {
            var product = _repository.Select(productId);

            var productResult = new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return Ok(productResult);
        }

        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll(int pageSize, int pageNumber)
        {
            List<ProductModel> productsModel = new();

            var products = _repository.SelectAll(pageSize, pageNumber);

            foreach (var product in products)
            {
                var productResult = new ProductModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                };

                productsModel.Add(productResult);
            }

            return Ok(productsModel);
        }

        [HttpPut("CreateOrUpdate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateOrUpdate(ProductModel productModel)
        {
            if (!ModelState.IsValid)
                return BadRequest("Sorry, the data model is invalid :(");
            
            var product = _mapper.Map<Product>(productModel);

            try
            {
                _repository.CreateOrUpdate(product);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return StatusCode(500);
            }

            if (string.IsNullOrWhiteSpace(productModel.Id))
                return CreatedAtAction(nameof(Get), new { Id = product.Id });

            return Ok(product);
        }

        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string productId)
        {
            if (!ModelState.IsValid)
                return BadRequest("Sorry, the data model is invalid :(");
            try
            {
                _repository.Delete(productId);
            }
            catch (RepositoryException ex)
            {
                _logger.LogError(ex.Message, ex.InnerException);
                return StatusCode(500);
            }

            return Ok(productId);
        }
    }
}
