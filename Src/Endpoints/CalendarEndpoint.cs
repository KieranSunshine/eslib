using System;
using System.Threading.Tasks;
using Eslib.Models;
using Eslib.Models.Internals;
using Eslib.Services;
using Eslib.Factories;
using Flurl;

namespace Eslib.Endpoints
{
    public class CalendarEndpoint : EndpointBase
    {
        #region Constructors

        public CalendarEndpoint(IDataService dataService, IAuthenticationService authenticationService) 
            : base(dataService, authenticationService)
        {
        }

        public CalendarEndpoint(
            IDataService dataService,
            IAuthenticationService authenticationService,
            IResponseFactory responseFactory)
            : base(dataService, authenticationService, responseFactory)
        {
        }

        #endregion

        public async Task<EsiResponse<EventSummary[]>> GetEventSummaries(int characterId, int? fromEvent = null)
        {
            var url = new Url()
                .AppendPathSegments("characters", characterId, "calendar");

            if (fromEvent.HasValue)
                url.SetQueryParam("from_event", fromEvent.Value);
            
            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<EventSummary[]>(response);
        }

        public async Task<EsiResponse<Event>> GetEvent(int characterId, int eventId)
        {
            var url = new Url()
                .AppendPathSegments("characters", characterId, eventId);

            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<Event>(response);
        }

        public async Task<EsiResponse<string>> RespondToEvent(int characterId, int eventId, Enums.Calendar.EventResponses eventResponse)
        {
            var url = new Url()
                .AppendPathSegments("characters", characterId, eventId);

            if (eventResponse is Enums.Calendar.EventResponses.NotResponded)
                throw new ArgumentOutOfRangeException(
                    nameof(eventResponse), 
                    "Cannot respond to an event with the NotResponded response");
            
            var response = await _dataService.PutAsync(url, eventResponse.ToString().ToLower());

            return _responseFactory.Create<string>(response);
        }

        public async Task<EsiResponse<EventAttendee[]>> GetEventAttendees(int characterId, int eventId)
        {
            var url = new Url()
                .AppendPathSegments("characters", characterId, eventId, "attendees");

            var response = await _dataService.GetAsync(url);

            return _responseFactory.Create<EventAttendee[]>(response);
        }
    }
}