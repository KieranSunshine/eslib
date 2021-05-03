using System;
using System.Threading.Tasks;
using Eslib.Models;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;

namespace Eslib.Endpoints
{
    public class CalendarEndpoint : EndpointBase
    {
        private const string endpoint = "calendar";

        public CalendarEndpoint(ApiOptions options) : base(options)
        {
        }

        public CalendarEndpoint(
            IDataService dataService,
            IRequestFactory requestFactory,
            IResponseFactory responseFactory)
            : base(dataService, requestFactory, responseFactory)
        {
        }

        public async Task<IResponse<EventSummary[]>> GetEventSummaries(int characterId, int? fromEvent = null)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), endpoint);

            if (fromEvent.HasValue)
            {
                request.AddQuery("from_event", fromEvent.Value.ToString());
            }

            var result = await _dataService.Get(request);

            return _responseFactory.Create<EventSummary[]>(result);
        }

        public async Task<IResponse<Event>> GetEvent(int characterId, int eventId)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), eventId.ToString());

            var result = await _dataService.Get(request);

            return _responseFactory.Create<Event>(result);
        }

        public async Task<IResponse<string>> RespondToEvent(int characterId, int eventId, Enums.Calendar.EventResponses response)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), eventId.ToString());

            if (response == Enums.Calendar.EventResponses.NotResponded)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(response), 
                    "Cannot respond to an event with the NotResponded response");
            }
            request.Data = response.ToString().ToLower();

            var result = await _dataService.Put(request);

            return _responseFactory.Create<string>(result);
        }

        public async Task<IResponse<EventAttendee[]>> GetEventAttendees(int characterId, int eventId)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), eventId.ToString(), "attendees");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<EventAttendee[]>(result);
        }
    }
}