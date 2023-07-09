using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace DynamoDB.Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDynamoDBContext _context;

        public ProductsController(IDynamoDBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}/{barcode}")]
        public async Task<IActionResult> Get(string id, string barcode)
        {
            var product = await _context.LoadAsync<Product>(id, barcode);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.ScanAsync<Product>(default).GetRemainingAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product request)
        {
            var product = await _context.LoadAsync<Product>(request.Id, request.Barcode);
            if (product != null) return BadRequest($"Product with Id {request.Id} and BarCode {request.Barcode} Already Exists");
            await _context.SaveAsync(request);
            return Ok(request);
        }

        [HttpDelete("{id}/{barcode}")]
        public async Task<IActionResult> Delete(string id, string barcode)
        {
            var product = await _context.LoadAsync<Product>(id, barcode);
            if (product == null) return NotFound();
            await _context.DeleteAsync(product);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product request)
        {
            var product = await _context.LoadAsync<Product>(request.Id, request.Barcode);
            if (product == null) return NotFound();
            await _context.SaveAsync(request);
            return Ok(request);
        }
    }
}
