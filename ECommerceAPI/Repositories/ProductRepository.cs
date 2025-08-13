using ECommerceAPI.Data;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Models;
public class ProductRepository : IProductRepository
{
    private AppDbContext appDbContext;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(AppDbContext appDbContext, ILogger<ProductRepository> logger)
    {
        this.appDbContext = appDbContext;
        _logger = logger;
    }

    public List<Product> GetProducts()
    {
        return appDbContext.Products.ToList();
    }
    public Product GetProduct(int id)
    {
        return appDbContext.Products.Find(id);
    }
    public bool Add(Product product)
    {
        var exists = appDbContext.Products.Where(p => product.Id == p.Id).FirstOrDefault();
        if (exists == null)
        {
            appDbContext.Products.Add(product);
            appDbContext.SaveChanges();
            return true;
        }
        throw new BadRequestException("Product already in database.");
    }
    public bool Remove(int id)
    {
        var exists = appDbContext.Products.Find(id);
        if (exists != null)
        {
            appDbContext.Products.Remove(exists);
            appDbContext.SaveChanges();
            return true;
        }
        throw new NotFoundException("Product not found in database and could not be removed.");

    }
    public bool Update(Product product)
    {
        var exists = appDbContext.Products.Find(product.Id);
        if (exists == null)
        {
            throw new NotFoundException("Product not found in database and could not be updated.");
        }
        exists.Name = product.Name;
        exists.Price = product.Price;
        exists.Stock = product.Price;
        exists.CategoryId = product.CategoryId;
        exists.Description = product.Description;
        appDbContext.SaveChanges();
        return true;
    }
}