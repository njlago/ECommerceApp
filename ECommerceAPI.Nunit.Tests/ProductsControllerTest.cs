using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace ECommerceAPI.Nunit.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly ProductsController _productController;

        public ProductsControllerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productController = new ProductsController(_productRepositoryMock.Object);
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            _productController.Dispose();
        }

        [Test]
        public void GetProducts()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", Price = 10 },
                new Product { Id = 2, Name = "Product 2", Price = 20 }
            };
            _productRepositoryMock.Setup(repo => repo.GetProducts()).Returns(products);

            var result = _productController.GetProducts().ToList();

            Assert.That(2, Is.EqualTo(result.Count()));
        }

        [Test]
        public void GetProduct_ValidId_ReturnsOkResult()
        {
            var product = new Product { Id = 1, Name = "Product 1", Price = 10 };
            _productRepositoryMock.Setup(repo => repo.GetProduct(1)).Returns(product);

            var result = _productController.GetProduct(1) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(product));
        }

        [Test]
        public void GetProduct_InvalidId_ReturnsBadRequest()
        {
            _productRepositoryMock.Setup(repo => repo.GetProduct(It.IsAny<int>())).Returns((Product)null);

            var result = _productController.GetProduct(999);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Product does not exist"));
        }

        [Test]
        public void GetProductByCategory_ValidCategoryId_ReturnsOkResult()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", CategoryId = 1 },
                new Product { Id = 2, Name = "Product 2", CategoryId = 1 }
            };
            _productRepositoryMock.Setup(repo => repo.GetProductByCategoryId(1)).Returns(products);

            var result = _productController.GetProductByCategory(1) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo(products));
        }

        [Test]
        public void GetProductByCategory_InvalidCategoryId_ReturnsInvalidCategoryRequest()
        {
            _productRepositoryMock.Setup(repo => repo.GetProductByCategoryId(It.IsAny<int>())).Returns((List<Product>)null);

            var result = _productController.GetProductByCategory(4);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("No Items with category 4."));
        }

        [Test]
        public void AddProduct_ValidProduct_ReturnsOkResult()
        {
            var product = new Product { Id = 1, Name = "New Product", Price = 10 };
            _productRepositoryMock.Setup(repo => repo.Add(product)).Returns(true);

            var result = _productController.Add(product) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Product was created successfully."));
        }

        [Test]
        public void AddProduct_InvalidProduct_ReturnsBadRequest()
        {
            var product = new Product { Id = 1, Name = "", Price = 10 };
            _productRepositoryMock.Setup(repo => repo.Add(product)).Returns(false);

            var result = _productController.Add(product);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Product could not be created."));
        }
        [Test]
        public void UpdateProduct_ValidProduct_ReturnsOkResult()
        {
            var product = new Product { Id = 1, Name = "Updated Product", Price = 15 };
            _productRepositoryMock.Setup(repo => repo.Update(product)).Returns(true);

            var result = _productController.Update(product) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Product was updated successfully."));
        }

        [Test]
        public void UpdateProduct_InvalidProduct_ReturnsBadRequest()
        {
            var product = new Product { Id = 1, Name = "", Price = 15 };
            _productRepositoryMock.Setup(repo => repo.Update(product)).Returns(false);

            var result = _productController.Update(product);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Product could not be updated."));
        }

        [Test]
        public void RemoveProduct_ValidId_ReturnsOkResult()
        {
            _productRepositoryMock.Setup(repo => repo.Remove(1)).Returns(true);

            var result = _productController.Remove(1) as OkObjectResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Value, Is.EqualTo("Product was removed successfully."));
        }

        [Test]
        public void RemoveProduct_InvalidId_ReturnsBadRequest()
        {
            _productRepositoryMock.Setup(repo => repo.Remove(999)).Returns(false);

            var result = _productController.Remove(999);

            Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
            Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("Product could not be removed."));
        }
    }
}
