using EventService.Models.Entities;

namespace EventService.Models.Interfaces
{
    public interface IBaseEventService
    {
        public bool CreateEvent(DateTime start, DateTime end, string title, string description, Guid idimage, Guid idspace);

        public bool UpdateEvent(Guid idevent,DateTime start, DateTime end, string title, string description, Guid idimage, Guid idspace);

        public bool DeleteEvent(Guid idevent);

        public List<Event> GetAllEvents();

        public List<Event> GetAllEventsForTheWeek();


    }
}
