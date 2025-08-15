using ECommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
// [Authorize]
[ApiController]
[Route("api")]
public class ProductsController : Controller
{
    private IProductRepository productRepository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
    {
        this.productRepository = productRepository;
        _logger = logger;
    }

    // public ProductsController(IProductRepository productRepository)
    // {
    //     this.productRepository = productRepository;
    // }
    // uncomment to test
    // [Authorize(Roles = "Admin, Customer")]
    [AllowAnonymous]
    [HttpGet("products")]

    public IEnumerable<Product> GetProducts()
    {
        return productRepository.GetProducts();
    }
    [Authorize(Roles = "Admin, Customer")]
    // [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var prod = productRepository.GetProduct(id);
        if (prod == null)
        {
            return BadRequest("Product does not exist");
        }
        return Ok(prod);
    }

    [Authorize(Roles = "Admin, Customer")]
    // [HttpGet("{cID}")]
    public IActionResult GetProductByCategory(int cID)
    {
        var prodList = productRepository.GetProductByCategoryId(cID);
        if (prodList == null)
        {
            return BadRequest($"No Items with category {cID}.");
        }
        return Ok(prodList);
    }

    // [Authorize(Roles = "Admin")]
    [HttpPost("admin/products")]
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

    // [Authorize(Roles = "Admin")]
    [HttpPut("admin/products/{id}")]
    public IActionResult Update([FromBody] Product prod)
    {   
        
        if (ModelState.IsValid)
        {

            bool updated = productRepository.Update(prod);
            if (updated)
            {

                return Ok(prod);
            }
        }
        return BadRequest("Product could not be updated.");
    }
    // [Authorize(Roles = "Admin")]
    [HttpDelete("admin/products/{id}")]
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