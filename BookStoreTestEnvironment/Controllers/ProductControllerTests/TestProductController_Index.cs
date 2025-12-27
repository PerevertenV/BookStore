using DataAccess.Entity;
using DataAccess.Models;
using JustStoreMVC.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreTestEnvironment.Controllers.ProductControllerTests
{
    [TestFixture]
    internal class TestProductController_Index : ProductTestSetup
    {
        private List<Product> productModels = new List<Product>();
        private List<ProductEntity> productEntities = new List<ProductEntity>();

        public override void Setup() 
        {
            base.Setup();

            productModels.Add(testModelObj);
            productEntities.Add(testEntityObj);

            _mockUnitOfWork.Setup(u => u.Product.GetAll(It.IsAny<Expression<Func<ProductEntity, bool>>>(),
                "Category")).Returns(productEntities);

            _mockMapper.Setup(m => m.Map<List<Product>>(productEntities)).Returns(productModels);
        }

        private IActionResult? getResultOfCallingIndex()
        {
            return _productController.Index();
        }

        private ViewResult? getViewResult()
        {
            return getResultOfCallingIndex() as ViewResult;
        }

        private List<Product>? getProductsFromModel()
        {
            return getViewResult().Model as List<Product>;
        }

        [Test]
        public void checkResultAsInstanceOfViewResult()
        {
            Assert.IsInstanceOf<ViewResult>(getResultOfCallingIndex());
        }

        [Test]
        public void checkViewResultIsNotNull()
        {
            Assert.IsNotNull(getViewResult());
        }

        [Test]
        public void checkListFromModelIsNotNull()
        {
            Assert.IsNotNull(getProductsFromModel());
        }

        [Test]
        public void checkListFromModelItsEqualToOriginalList()
        {
            Assert.That(productModels, Is.EqualTo(getProductsFromModel()));
        }

        [Test]
        public void verifyCallingOfMapper()
        {
            getResultOfCallingIndex();
            _mockMapper.Verify(v => v.Map<List<Product>>(productEntities), Times.Once);
        }

    }
}
