using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Seller.API.Entity;
using Seller.API.Exceptions;
using Seller.API.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ILogger<SellerController> _logger;

        public SellerController(IProductService productService, ILogger<SellerController> logger)
        {
            this.productService = productService;
            _logger = logger;
        }

        [HttpGet("{productId}", Name ="GetProduct")]
        public async Task<ActionResult<Product>> Get(string productId)
        {
            try
            {
                _logger.LogInformation($"Getting product details with Id {productId}");
                var product = await productService.GetProductById(productId);
                return Ok(product);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("add-Product")]
        public async Task<ActionResult> AddProduct([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation($"Adding the new product {product}");
                    await productService.CreateProduct(product);
                    return Created("", "Product is added successfully");
                }
                catch (BidEndDateException ex)
                {
                    _logger.LogError(ex.Message);
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    return StatusCode(500);
                }

            }
            var errorMessages = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
            _logger.LogError($"Error in Model Binding : {errorMessages}");
            return BadRequest(ModelState);
        }

        [HttpDelete("delete/{productId}")]
        public async Task<ActionResult> DeleteProduct(string productId)
        {
            try
            {
                _logger.LogInformation($"Deleting the product {productId}");
                await productService.DeleteProduct(productId);
                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

    }
}
