using System.Threading.Tasks;
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

        public async Task<Response<EventSummary[]>> GetEventSummaries(int characterId, int? fromEvent = null)
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

        public async Task<Response<Event>> GetEvent(int characterId, int eventId)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), eventId.ToString());

            var result = await _dataService.Get(request);

            return _responseFactory.Create<Event>(result);
        }

        public async Task<Response<string>> RespondToEvent(int characterId, int eventId, string response)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), eventId.ToString());
            
            // TODO: check response against acceptable enum.
            // TODO: investigate ways of returning responses that are only a response code.
            
            request.Data = response;

            var result = await _dataService.Put(request);

            return _responseFactory.Create<string>(result);
        }

        public async Task<Response<EventResponse[]>> GetEventAttendees(int characterId, int eventId)
        {
            var request = _requestFactory.Create()
                .AddPaths("characters", characterId.ToString(), eventId.ToString(), "attendees");

            var result = await _dataService.Get(request);

            return _responseFactory.Create<EventResponse[]>(result);
        }
    }
}