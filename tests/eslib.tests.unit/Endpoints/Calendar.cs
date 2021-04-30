using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib;
using Eslib.Endpoints;
using Eslib.Models;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Services.Factories;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Endpoints
{
    [TestFixture]
    public class CalendarTests
    {
        private Request _stubbedRequest;

        private Mock<IDataService> _mockDataService;
        private Mock<IRequestFactory> _mockRequestFactory;
        private Mock<IResponseFactory> _mockResponseFactory;
        
        [SetUp]
        public void Init()
        {
            _stubbedRequest = new Request();

            _mockDataService = new Mock<IDataService>();
            _mockRequestFactory = new Mock<IRequestFactory>();
            _mockResponseFactory = new Mock<IResponseFactory>();

            _mockRequestFactory.Setup(m => m.Create())
                .Returns(_stubbedRequest);
        }

        [Test]
        public async Task GetEventSummaryTest()
        {
            var stubbedData = new EventSummary[]
            {
                new EventSummary()
                {
                    Id = 1,
                    Title = "Fake Event",
                    EventDate = new DateTime(2021, 1, 31).ToString(),
                    Response = Enums.Calendar.EventResponses.Accepted,
                    Importance = 1
                }
            };
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<EventSummary[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<EventSummary[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var calendarEndpoint = new CalendarEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await calendarEndpoint.GetEventSummaries(1);
            
            Assert.IsTrue(result.Success);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsNotNull(result.Data);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.AreEqual(stubbedData, result.Data);
        }

        [Test]
        public async Task GetEvent()
        {
            var stubbedData = new Event(
                new DateTime(2021, 1, 31).ToString(),
                5, 
                1,
                1,
                1,
                "Some Owner",
                Enums.Calendar.OwnerType.Character,
                Enums.Calendar.EventResponses.Declined,
                "Some text",
                "Some Title");

            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<Event>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Event>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var calendarEndpoint = new CalendarEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await calendarEndpoint.GetEvent(1, 1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(stubbedData, result.Data);
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
        }
        
        [Test]
        public async Task RespondToEventTest()
        {
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NoContent
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<string>(HttpStatusCode.NoContent);

            _mockDataService
                .Setup(m => m.Put(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var calendarEndpoint = new CalendarEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await calendarEndpoint.RespondToEvent(
                1, 
                1, 
                Enums.Calendar.EventResponses.Accepted);
            
            Assert.IsTrue(result.Success);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            
            Assert.IsNull(result.Message);
            Assert.IsNull(result.Data);
            Assert.IsNull(result.Error);
        }

        [Test]
        public async Task GetEventAttendees()
        {
            var stubbedData = new []
            {
                new EventAttendee()
                {
                    CharacterId = 1,
                    Response = Enums.Calendar.EventResponses.Accepted
                },
                new EventAttendee()
                {
                    CharacterId = 2,
                    Response = Enums.Calendar.EventResponses.Declined
                }
            };

            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonSerializer.Serialize(stubbedData))
            };
            var httpResponseTask = Task.FromResult(httpResponse);
            var response = new Response<EventAttendee[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.Get(It.IsAny<Request>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<EventAttendee[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var calendarEndpoint = new CalendarEndpoint(
                _mockDataService.Object,
                _mockRequestFactory.Object,
                _mockResponseFactory.Object);

            var result = await calendarEndpoint.GetEventAttendees(1, 1);
            
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(stubbedData, result.Data);
        }
    }
}