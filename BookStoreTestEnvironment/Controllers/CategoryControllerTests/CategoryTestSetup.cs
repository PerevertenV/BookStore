using DataAccess.Entity;
using DataAccess.Models;
using JustStoreMVC.Areas.Admin.Controllers;
using Moq;
using System.Linq.Expressions;

namespace BookStoreTestEnvironment.Controllers.CategoryControllerTests
{
    internal class CategoryTestSetup : SetupForControllers<Category, CategoryEntity>
    {
        protected CategoryController _categoryController;
        protected Category incorrectCategory;
        protected CategoryEntity sendedToDbCategory;
        protected int correctCategoryId;
        protected int inccorectCategoryId;

        [SetUp]
        public virtual void Setup()
        {

            correctCategoryId = 1;
            inccorectCategoryId = 0;

            testModelObj = new Category { ID = 1, Name = "category", DisplayOrder = 2 };
            testEntityObj = new CategoryEntity { ID = 1, Name = "category", DisplayOrder = 2 };
            incorrectCategory = new Category() { ID = 2, Name = "1", DisplayOrder = 1 };
            sendedToDbCategory = new CategoryEntity();

            base.Setup();

            _categoryController = new CategoryController(_mockUnitOfWork.Object, _mockMapper.Object);

            _categoryController.TempData = tempData;

            _mockUnitOfWork.Setup(u => u.Category.GetFirstOrDefault(It.IsAny<Expression<Func<CategoryEntity,
                bool>>>(), null, false)).Returns(testEntityObj);
        }

        [TearDown]
        protected void TearDown()
        {
            _categoryController.Dispose();
        }

    }
}
