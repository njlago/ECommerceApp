using ECommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// [Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductController : Controller
{
    private IProductRepository productRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
    {
        this.productRepository = productRepository;
        _logger = logger;
    }

    // [Authorize(Roles = "Admin, Customer")]
    [AllowAnonymous]
    [HttpGet]
    public IEnumerable<Product> GetProducts()
    {
        return productRepository.GetProducts();
    }
    [Authorize(Roles = "Admin, Customer")]
    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var prod = productRepository.GetProduct(id);
        if (prod == null)
        {
            return BadRequest("Product does not exist");
        }
        return Ok(prod);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult Add(Product product)
    {
        if (ModelState.IsValid)
        {
            bool created = productRepository.Add(product);
            if (created)
            {
                return Ok("Product was created successfully.");
            }
        }
        return BadRequest("Product could not be created.");
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public IActionResult Update(Product product)
    {
        if (ModelState.IsValid)
        {
            bool updated = productRepository.Update(product);
            if (updated)
            {
                return Ok("Product was updated successfully.");
            }
        }
        return BadRequest("Product could not be updated.");
    }
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public IActionResult Remove(int id)
    {
        
        bool removed = productRepository.Remove(id);
        if (removed)
        {
            return Ok("Product was removed successfully.");
        }
        return BadRequest("Product could not be removed.");
    }
}