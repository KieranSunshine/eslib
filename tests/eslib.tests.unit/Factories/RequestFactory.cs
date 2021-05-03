using Eslib.Models.Internals;
using Eslib.Factories;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Factories
{
    [TestFixture]
    public class RequestFactoryTests
    {
        private string _apiUrl = "http://testurl.com";
        private string _version = "latest";

        private IRequestFactory _requestFactory;
        
        [SetUp]
        public void Init()
        {
            var options = new ApiOptions
            {
                ApiUrl = _apiUrl,
                Version = _version
            };
            
            _requestFactory = new RequestFactory(options);
        }

        [Test]
        public void CreatedResponseReturnsAsExpected()
        {
            var result = _requestFactory.Create();
            
            Assert.AreEqual($"{_apiUrl}/{_version}/?datasource=tranquility", result.Url);
        }
    }
}