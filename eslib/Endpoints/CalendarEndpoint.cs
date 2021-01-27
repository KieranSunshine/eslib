using eslib.Models;
using eslib.Models.Internals;
using eslib.Services;
using eslib.Services.Factories;

namespace eslib.Endpoints
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

        public Response<EventSummary[]> GetEventSummaries(int characterId, int? fromEvent = null)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), endpoint);

            if (fromEvent.HasValue) request.AddQuery("from_event", fromEvent.Value.ToString());

            var result = _dataService.Get(request).Result;

            return _responseFactory.Create<EventSummary[]>(result);
        }

        public Response<Event> GetEvent(int characterId, int eventId)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), eventId.ToString());

            var result = _dataService.Get(request).Result;

            return _responseFactory.Create<Event>(result);
        }

        public Response<EventResponse[]> GetEventAttendees(int characterId, int eventId)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), eventId.ToString(), "attendees");

            var result = _dataService.Get(request).Result;

            return _responseFactory.Create<EventResponse[]>(result);
        }
    }
}