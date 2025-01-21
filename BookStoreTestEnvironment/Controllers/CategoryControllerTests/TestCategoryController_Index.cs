using AutoMapper;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using JustStore.Models;
using JustStoreMVC.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreTestEnvironment.Controllers.CategoryControllerTests
{
    [TestFixture]
    internal class TestCategoryController_Index : CategoryTestSetup
    {
        private List<Category> categorysModel;
        private List<CategoryEntity> categorysEntity;

        [SetUp]
        public override void Setup()
        {
            base.Setup();

            categorysModel = new List<Category>();
            categorysEntity = new List<CategoryEntity>();

            categorysModel.Add(testModelObj);
            categorysEntity.Add(testEntityObj);

            _mockUnitOfWork.Setup(u => u.Category.GetAll(It.IsAny<Expression<Func<CategoryEntity, bool>>>(),
                null)).Returns(categorysEntity);

            _mockMapper.Setup(m => m.Map<List<Category>>(categorysEntity)).Returns(categorysModel);
        }

        private IActionResult? getResultOfCallingIndex()
        {
            return _categoryController.Index();
        }

        private ViewResult? getViewResult()
        {
            return getResultOfCallingIndex() as ViewResult;
        }

        private List<Category>? getCategoriesFromModel()
        {
            return getViewResult().Model as List<Category>;
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
            Assert.IsNotNull(getCategoriesFromModel());
        }

        [Test]
        public void checkListFromModelItsEqualToOriginalList()
        {
            Assert.AreEqual(getCategoriesFromModel(), categorysModel);
        }

        [Test]
        public void verifyCallingOfMapper() 
        {
            getResultOfCallingIndex();
            _mockMapper.Verify(v => v.Map<List<Category>>(categorysEntity), Times.Once);
        }
    }
}