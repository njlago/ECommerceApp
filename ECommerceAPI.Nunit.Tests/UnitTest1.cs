using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
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
        public void GetProducts()
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
    }
}
