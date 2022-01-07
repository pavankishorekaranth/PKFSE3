using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Seller.Application.Commands;
using Seller.Application.Exceptions;
using Seller.Application.Queries;
using Seller.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seller.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SellerController : ControllerBase
    {
        private readonly ILogger<SellerController> _logger;
        private readonly IMediator _mediator;

        public SellerController(ILogger<SellerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("GetAllProducts", Name = "GetAllProducts")]
        public async Task<ActionResult<List<ProductDetails>>> Get()
        {
            try
            {
                _logger.LogInformation($"Getting all product details");
                var query = new GetAllProductsQuery();
                var productsList = await _mediator.Send(query);

                return Ok(productsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpGet("ShowBids/{productId}", Name = "ShowBids")]
        public async Task<ActionResult<ProductBidDetails>> Get(string productId)
        {
            try
            {
                _logger.LogInformation($"Getting product details with Id {productId}");
                var query = new GetAllBidsForProductQuery(productId);
                var product = await _mediator.Send(query);

                return Ok(product);
            }
            catch (ProductNotExist ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddProduct([FromBody] CreateProductCommand product)
        {
            try
            {
                _logger.LogInformation("Creating Product");
                var result = await _mediator.Send(product);
                _logger.LogInformation($"Product is created with Id {result}");
                return Ok(result);
            }
            catch (BidEndDateException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (MustBeNumberException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch(ValidationException ex)
            {
                _logger.LogError(ex.Message);
                var items = ex.Errors.SelectMany(d => d.Value).ToList();
                return BadRequest(string.Join(";", items));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<ActionResult> DeleteProduct(string productId)
        {
            try
            {
                _logger.LogInformation($"Deleting the product {productId}");
                var command = new DeleteProductCommand() { ProductId = productId };
                await _mediator.Send(command);

                return Ok("Product is deleted successfully");
            }
            catch(ProductNotExist ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (DeleteProductAfterBidEndDate ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (DeleteProductForAtleastOneBidException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

    }
}
