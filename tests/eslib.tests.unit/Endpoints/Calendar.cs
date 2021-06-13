using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Eslib.Endpoints;
using Eslib.Models;
using Eslib.Models.Internals;
using Flurl;
using Moq;
using NUnit.Framework;

namespace Eslib.Tests.Unit.Endpoints
{
    [TestFixture]
    public class CalendarTests : EndpointTestBase<CalendarEndpoint>
    {
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
            var response = new EsiResponse<EventSummary[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.GetAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<EventSummary[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var result = await Target.GetEventSummaries(1);
            
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
            var response = new EsiResponse<Event>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.GetAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<Event>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var result = await Target.GetEvent(1, 1);

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
            var response = new EsiResponse<string>(HttpStatusCode.NoContent);

            _mockDataService
                .Setup(m => m.PutAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<string>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);
            
            var result = await Target.RespondToEvent(
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
            var response = new EsiResponse<EventAttendee[]>(HttpStatusCode.OK, stubbedData);

            _mockDataService
                .Setup(m => m.GetAsync(It.IsAny<Url>()))
                .Returns(httpResponseTask);

            _mockResponseFactory
                .Setup(m => m.Create<EventAttendee[]>(It.IsAny<HttpResponseMessage>()))
                .Returns(response);

            var result = await Target.GetEventAttendees(1, 1);
            
            Assert.IsNull(result.Error);
            Assert.IsNull(result.Message);
            Assert.IsTrue(result.Success);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(stubbedData, result.Data);
        }
    }
}