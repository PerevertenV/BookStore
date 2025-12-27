using DataAccess.Entity;
using DataAccess.Models;
using JustStoreMVC.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System.Linq.Expressions;

namespace BookStoreTestEnvironment.Controllers.ProductControllerTests
{
    internal class ProductTestSetup: SetupForControllers<Product, ProductEntity>
    {

        protected ProductController _productController;
        protected Mock<IWebHostEnvironment> _mockIWebEnv;
        protected Category categoryForProductModel;
        protected CategoryEntity categoryForProductEntity;

        [SetUp]
        public virtual void Setup()
        {
            _mockIWebEnv = new Mock<IWebHostEnvironment>();
            categoryForProductModel = new Category() { ID = 1, Name = "category", DisplayOrder = 2 };
            categoryForProductEntity = new CategoryEntity() { ID = 1, Name = "category", DisplayOrder = 2 };

            testModelObj = new Product() 
            {
                Id = 1, 
                Author="1x1", 
                CategoryId = 1, 
                Category = categoryForProductModel, 
                Description = "111",
                ISBN="222",
                ListPrice = 1, 
                Price = 2,
                Price100 = 1,
                Price50 = 1, 
                Title = "Tini Zabutyh predkiv"
            };

            testEntityObj = new ProductEntity()
            {
                ID = 1,
                Author = "1x1",
                CategoryId = 1,
                Category = categoryForProductEntity,
                Description = "111",
                ISBN = "222",
                ListPrice = 1,
                Price = 2,
                Price100 = 1,
                Price50 = 1,
                Title = "Tini Zabutyh predkiv"
            };

            _productController = new ProductController(_mockUnitOfWork.Object, 
                _mockIWebEnv.Object, _mockMapper.Object);

            _mockUnitOfWork.Setup(u => u.Product.GetFirstOrDefault(It.IsAny<Expression<Func<ProductEntity,
                bool>>>(), null, false)).Returns(testEntityObj);
        }

        [TearDown]
        protected void TearDown()
        {
            _productController.Dispose();
        }

    }
}
