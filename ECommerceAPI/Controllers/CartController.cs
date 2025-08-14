using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private static List<CartItem> cartItems = new();

    [HttpGet]
    public IActionResult Get() => Ok(cartItems);

    [HttpPost]
    public IActionResult Add(CartItem item)
    {
        var existing = cartItems.FirstOrDefault(x => x.ProductId == item.ProductId);
        if (existing != null)
        {
            existing.Quantity += item.Quantity;
        }
        else
        {
            item.Id = cartItems.Count + 1;
            cartItems.Add(item);
        }
        return Ok(cartItems);
    }

    [HttpPut("{productId}")]
    public IActionResult Update(int productId, [FromBody] int quantity)
    {
        var item = cartItems.FirstOrDefault(x => x.ProductId == productId);
        if (item == null) return NotFound();
        item.Quantity = quantity;
        return Ok(item);
    }

    [HttpDelete("{productId}")]
    public IActionResult Remove(int productId)
    {
        cartItems = cartItems.Where(x => x.ProductId != productId).ToList();
        return Ok(cartItems);
    }

    [HttpDelete]
    public IActionResult Clear()
    {
        cartItems.Clear();
        return Ok();
    }
}
