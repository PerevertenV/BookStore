using DataAccess.Entity;
using JustStore.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace BookStoreTestEnvironment.Controllers.CategoryControllerTests
{
    [TestFixture]
    internal class TestCategoryController_EditCategory : CategoryTestSetup
    {
        public override void Setup() 
        {
            base.Setup();

            _mockUnitOfWork.Setup(u => u.Category.Update(It.IsAny<CategoryEntity>()))
                .Callback<CategoryEntity>(category => sendedToDbCategory = category);
        }

        private IActionResult callEditCategoryWithCorrectId() 
        {
            return _categoryController.EditCategory(correctCategoryId);
        }
        [Test]
        public void checkCallOfMapper()
        {
            callEditCategoryWithCorrectId();
            _mockMapper.Verify(m => m.Map<Category>(testEntityObj), Times.Once);
        }
        [Test]
        public void checkInstanceOfViewResult()
        {
            Assert.IsInstanceOf<ViewResult>(callEditCategoryWithCorrectId());
        }
        [Test]
        public void checkResultIsNotNull()
        {
            Assert.IsNotNull(callEditCategoryWithCorrectId());
        }

        private IActionResult callEditCategoryWithNotCorrectId() 
        {
            return _categoryController.EditCategory(inccorectCategoryId);
        }
        [Test]
        public void checkNotFoundWithZeroId()
        {
            Assert.IsInstanceOf<NotFoundResult>(callEditCategoryWithNotCorrectId());
        }

        private void returnNotFoundFromDb()
        { 
            _mockMapper.Setup(m => m.Map<Category>(testEntityObj)).Returns((Category)null);
        }
        [Test]
        public void checkNotFoundTempData()
        {
            returnNotFoundFromDb();
            callEditCategoryWithCorrectId();

            Assert.That(_categoryController.TempData["error"], Is.EqualTo("Category wasn`t updated"));
        }
        [Test]
        public void checkNotFoundResult()
        {
            returnNotFoundFromDb();

            Assert.IsInstanceOf<NotFoundResult>(callEditCategoryWithCorrectId());
        }

        private Category returnCategoryFromViewResult() 
        {
            var viewResult = callEditCategoryWithCorrectId() as ViewResult;
            return viewResult.Model as Category;
        }
        [Test]
        public void compareOriginalCategoryAndFromViewResult()
        {
            Assert.That(returnCategoryFromViewResult(), Is.EqualTo(testModelObj));
        }

        private IActionResult callEditCategoryWithCorrectObj()
        {
            return _categoryController.EditCategory(testModelObj);
        }
        [Test]
        public void checkCallOfSaveObj()
        {
            callEditCategoryWithCorrectObj();
            _mockUnitOfWork.Verify(u => u.save(), Times.Once());
        }
        [Test]
        public void checkSuccesUpdatedTempData()
        {
            callEditCategoryWithCorrectObj();
            Assert.That(_categoryController.TempData["success"], Is.EqualTo("Category updated successfully"));
        }
        [Test]
        public void comapareUpdatedCategorySendedToDbAndOriginal()
        {
            callEditCategoryWithCorrectObj();
            Assert.That(sendedToDbCategory, Is.EqualTo(testEntityObj));
        }
        [Test]
        public void checkCorrectRedirection()
        {
            var actionNameOfRedirection = (callEditCategoryWithCorrectObj() as RedirectToActionResult).ActionName;

            Assert.That(actionNameOfRedirection, Is.EqualTo("Index"));
        }

        private IActionResult callEditCategoryWithNotCorrectObj()
        {
            return _categoryController.EditCategory(incorrectCategory);
        }
        [Test]
        public void checkErrorModelState()
        {
            callEditCategoryWithNotCorrectObj();
            Assert.IsFalse(_categoryController.ModelState.IsValid);
        }
        [Test]
        public void returnAfterIncorrectCategorryIsNotNull()
        {
            Assert.IsNotNull(callEditCategoryWithNotCorrectObj());
        }
    }
}
