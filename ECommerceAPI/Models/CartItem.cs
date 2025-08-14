public class CartItem
{
    public int Id { get; set; } // Cart item ID
    public int ProductId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
