using ECommerceAPI.Models;

public interface IProductRepository
{
    bool Add(Product product);
    bool Remove(int id);
    bool Update(Product product);
    List<Product> GetProducts();
    Product GetProduct(int id);
    
}