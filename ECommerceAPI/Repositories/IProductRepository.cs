using ECommerceAPI.Models;

public interface IProductRepository
{
    bool Add(Product product);
    bool Remove(int id);
    bool Update(Product product);
    List<Product> GetProducts();
    List<Product> GetProductByCategoryId(int cID);
    Product GetProduct(int id);
    
}