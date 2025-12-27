using DataAccess.Entity;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Linq.Expressions;

namespace BookStoreTestEnvironment.Controllers.CategoryControllerTests
{
    [TestFixture]
    internal class TestCategoryController_DeleteCategory : CategoryTestSetup
    {
        public override void Setup() 
        {
            base.Setup();

            _mockUnitOfWork.Setup(u => u.Category.Delete(It.IsAny<CategoryEntity>()))
                .Callback<CategoryEntity>(category => sendedToDbCategory = category);

            _mockUnitOfWork.Setup(u => u.Category.GetFirstOrDefault(It
                .IsAny<Expression<Func<CategoryEntity, bool>>>(), null, false))
                .Returns((Expression<Func<CategoryEntity, bool>> predicate, string include, bool tracking) =>
                {
                    var compiledPredicate = predicate.Compile();

                    if (compiledPredicate(testEntityObj))
                    {
                        return testEntityObj; 
                    }

                    return (CategoryEntity)null;
                });
        }

        private IActionResult callDeleteCategoryWithCorrectId()
        {
            return _categoryController.DeleteCategory(correctCategoryId);
        }
        [Test]
        public void checkCallOfMapper()
        {
            callDeleteCategoryWithCorrectId();
            _mockMapper.Verify(m => m.Map<Category>(testEntityObj), Times.Once);
        }
        [Test]
        public void checkInstanceOfViewResult()
        {
            Assert.IsInstanceOf<ViewResult>(callDeleteCategoryWithCorrectId());
        }
        [Test]
        public void checkResultIsNotNull()
        {
            Assert.IsNotNull(callDeleteCategoryWithCorrectId());
        }

        private Category returnCategoryFromViewResult()
        {
            var viewResult = callDeleteCategoryWithCorrectId() as ViewResult;
            return viewResult.Model as Category;
        }
        [Test]
        public void compareOriginalCategoryAndFromViewResult()
        {
            Assert.That(returnCategoryFromViewResult(), Is.EqualTo(testModelObj));
        }

        private IActionResult callDeleteCategoryWithNotCorrectId()
        {
            return _categoryController.DeleteCategory(inccorectCategoryId);
        }
        [Test]
        public void checkNotFoundWithZeroId()
        {
            Assert.IsInstanceOf<NotFoundResult>(callDeleteCategoryWithNotCorrectId());
        }

        private void returnNotFoundFromDb()
        {
            _mockMapper.Setup(m => m.Map<Category>(testEntityObj)).Returns((Category)null);
        }
        [Test]
        public void checkNotFoundResult()
        {
            returnNotFoundFromDb();

            Assert.IsInstanceOf<NotFoundResult>(callDeleteCategoryWithCorrectId());
        }

        private IActionResult? callDeleteCategoryPostWithCorrectId()
        {
            return _categoryController.DeleteCategoryPost(correctCategoryId);
        }
        [Test]
        public void checkCallOfSaveObj()
        {
            callDeleteCategoryPostWithCorrectId();
            _mockUnitOfWork.Verify(u => u.save(), Times.Once());
        }
        [Test]
        public void checkSuccessDeletedTempData()
        {
            callDeleteCategoryPostWithCorrectId();
            Assert.That(_categoryController.TempData["success"], Is.EqualTo("Category deleted successfully"));
        }
        [Test]
        public void compareDeletedCategorySenеToDbAndOriginal()
        {
            callDeleteCategoryPostWithCorrectId();
            Assert.That(sendedToDbCategory, Is.EqualTo(testEntityObj));
        }
        [Test]
        public void checkCorrectRedirection()
        {
            var actionNameOfRedirection = (callDeleteCategoryPostWithCorrectId()  as RedirectToActionResult)
                .ActionName;

            Assert.That(actionNameOfRedirection, Is.EqualTo("Index"));
        }

        private IActionResult callDeleteCategoryPOSTyWithNotCorrectId()
        {
            return _categoryController.DeleteCategoryPost(inccorectCategoryId);
        }
        [Test]
        public void checkErrorTempDataAfterDeleteting()
        {
            callDeleteCategoryPOSTyWithNotCorrectId();
            Assert.That(_categoryController.TempData["error"], Is.EqualTo("Category wasn`t deleted"));
        }
        [Test]
        public void returnAfterIncorrectCategorryIsNotNull()
        {
            Assert.IsNotNull(callDeleteCategoryPOSTyWithNotCorrectId());
        }


    }
}
