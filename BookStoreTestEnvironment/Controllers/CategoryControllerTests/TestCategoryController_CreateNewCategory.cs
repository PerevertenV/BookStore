using AutoMapper;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using JustStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreTestEnvironment.Controllers.CategoryControllerTests
{
    [TestFixture]
    internal class TestCategoryController_CreateNewCategory : CategoryTestSetup
    {
        public override void Setup()
        {
            base.Setup();

            _mockUnitOfWork.Setup(u => u.Category.Add(It.IsAny<CategoryEntity>()))
                .Callback<CategoryEntity>(category => sendedToDbCategory = category);

        }

        public RedirectToActionResult callCreateWithCorrectObj() 
        {
            return _categoryController.CreateNewCategory(testModelObj) as RedirectToActionResult;
        }

        public ViewResult? callMethodWithInccorectCategory() 
        {
            return _categoryController.CreateNewCategory(incorrectCategory) as ViewResult;
        }

        [Test]
        public void checkThatResultIsNotNull() 
        {
            Assert.IsNotNull(callCreateWithCorrectObj());
        }

        [Test]
        public void compareOriginallAndAddedCategory() 
        {
            callCreateWithCorrectObj();
            Assert.That(sendedToDbCategory, Is.EqualTo(testEntityObj));
        }

        [Test]
        public void verefyCallingOfMethods() 
        {
            callCreateWithCorrectObj();
            _mockUnitOfWork.Verify(v => v.save(), Times.Once);
            _mockMapper.Verify(v => v.Map<CategoryEntity>(testModelObj), Times.Once);
        }

        [Test]
        public void checkCorrectRedirection()
        {
            Assert.That(callCreateWithCorrectObj().ActionName, Is.EqualTo("Index"));
        }

        [Test]
        public void checkTempDataMessage() 
        {
            callCreateWithCorrectObj();
            Assert.That(_categoryController.TempData["success"], Is.EqualTo("Category created successfully"));
        }

        [Test]
        public void checkErrorModelState() 
        {
            callMethodWithInccorectCategory();
            Assert.IsFalse(_categoryController.ModelState.IsValid);
        }

        [Test]
        public void returnAfterIncorrectCategorryIsNotNull() 
        {
            Assert.IsNotNull(callMethodWithInccorectCategory());   
        }
    }
}
