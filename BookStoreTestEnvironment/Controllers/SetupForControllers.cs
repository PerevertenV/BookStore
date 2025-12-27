using AutoMapper;
using DataAccess.Entity;
using DataAccess.Repository.IRepository;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace BookStoreTestEnvironment.Controllers
{
    internal abstract class SetupForControllers<T, M> where T: class
    {
        protected Mock<IUnitOfWork> _mockUnitOfWork;
        protected Mock<IMapper> _mockMapper;
        protected Mock<ITempDataProvider> _tempDataProviderMock;
        protected TempDataDictionary? tempData;
        protected T testModelObj;
        protected M testEntityObj;

        [SetUp]
        public virtual void Setup() 
        {
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _tempDataProviderMock = new Mock<ITempDataProvider>();

            tempData = new TempDataDictionary(new DefaultHttpContext(), _tempDataProviderMock.Object);

            _mockUnitOfWork.Setup(u => u.save()).Verifiable();

            _mockMapper.Setup(m => m.Map<T>(testEntityObj)).Returns(testModelObj);
            _mockMapper.Setup(m => m.Map<M>(testModelObj)).Returns(testEntityObj);
        }
    }
}
