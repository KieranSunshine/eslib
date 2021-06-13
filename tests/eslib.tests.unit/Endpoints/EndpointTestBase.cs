using System;
using Eslib.Factories;
using Eslib.Services;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Endpoints
{
    public abstract class EndpointTestBase<T>
    {
        protected Mock<IDataService> _mockDataService;
        protected Mock<IAuthenticationService> _mockAuthenticationService;
        protected Mock<IResponseFactory> _mockResponseFactory;

        protected T Target => CreateInstance();

        [SetUp]
        public void Init()
        {
            _mockDataService = new Mock<IDataService>();
            _mockAuthenticationService = new Mock<IAuthenticationService>();
            _mockResponseFactory = new Mock<IResponseFactory>();
        }

        private T CreateInstance()
        {
            return (T)Activator.CreateInstance(typeof(T),
                _mockDataService.Object,
                _mockAuthenticationService.Object,
                _mockResponseFactory.Object);
        }
    }
}