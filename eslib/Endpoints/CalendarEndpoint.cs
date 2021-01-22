using eslib.Models;
using eslib.Services;

namespace eslib.Endpoints
{
    public class CalendarEndpoint
    {
        protected readonly IDataService _dataService;

        public CalendarEndpoint(IDataService dataService)
        {
            _dataService = dataService;
        }

        public Response<EventSummary[]> GetEventSummaries(int id)
        {
            var url = _dataService.GenerateUrl("characters", id.ToString(), "calendar");
            var result = _dataService.Get<EventSummary[]>(url).Result;

            return result;
        }
    }
}