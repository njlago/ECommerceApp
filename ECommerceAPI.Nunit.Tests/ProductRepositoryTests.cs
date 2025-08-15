using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
namespace ECommerceAPI.Nunit.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test1()
        {
            Product product = new Product();
            Assert.Pass();
        }

        [Test]
        public void GetProductsTest()
        {
            var data = new List<Product>
            {
                new Product { Id = 1, Name = "AAA", CategoryId = 3, Description = "aaa", Price = 1, Stock = 1 },
                new Product { Id = 2, Name = "BBB" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductRepository(mockContext.Object);
            var blogs = service.GetProducts();

            Assert.That(2, Is.EqualTo(blogs.Count));
            Assert.That("AAA", Is.EqualTo(blogs[0].Name));
            Assert.That("BBB", Is.EqualTo(blogs[1].Name));
        }

        [Test]
        public void RemoveProductsTest()
        {
            var data = new List<Product>
            {
                new Product { Id = 1, Name = "AAA", CategoryId = 3, Description = "aaa", Price = 1, Stock = 1 },
                new Product { Id = 2, Name = "BBB" },

            };

            var mockSet = new Mock<DbSet<Product>>();
            var mockContext = new Mock<AppDbContext>();

            mockSet.Setup(m => m.Find(1)).Returns(data.FirstOrDefault(p => p.Id == 1));
            mockSet.Setup(m => m.Find(2)).Returns(data.FirstOrDefault(p => p.Id == 2));

            mockSet.Setup(m => m.Remove(It.IsAny<Product>())).Callback<Product>(p => data.Remove(p));

            mockContext.Setup(c => c.SaveChanges()).Returns(1);

            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductRepository(mockContext.Object);

            Assert.That(service.Remove(1), Is.EqualTo(true));

            Assert.That(data.Count, Is.EqualTo(1));
            Assert.That(data.Any(p => p.Id == 1), Is.False);
            Assert.That(data.FirstOrDefault(p => p.Id == 2), Is.Not.Null);
        }

        [Test]
        public void RemoveProduct_NonExistentId_ThrowsNotFoundException()
        {
            var data = new List<Product>
            {
                new Product { Id = 1, Name = "AAA", CategoryId = 3, Description = "aaa", Price = 1, Stock = 1 },
                new Product { Id = 2, Name = "BBB" },

            }.AsQueryable();

            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<AppDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var service = new ProductRepository(mockContext.Object);

            Assert.That(() => service.Remove(999), Throws.TypeOf<NotFoundException>());
        }
    }
}
