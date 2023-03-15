using EventService.Models.Entities;
using EventService.Models.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace EventService.Models.Interfaceimplements
{
    public class BaseEventService:IBaseEventService
    {
        private List<Event> _events;
        
        public BaseEventService() { _events = new List<Event>(); }

        public bool CreateEvent(DateTime start, DateTime end, string title, string description, Guid idimage, Guid idspace)
        {
            _events.Add(new Event { Start = start, End = end, Title=title, Description = description, IdImage = idimage, IdSpace = idspace });
            return _events.Any(v => v.Title == title);
        }

        public bool DeleteEvent(Guid idevent)
        {
            var removablEvent = _events.FirstOrDefault(v => v.Id == idevent);
            return _events.Remove(removablEvent);
           
        }

        public List<Event> GetAllEvents()
        {
            return _events.ToList();
        }

        public List<Event> GetAllEventsForTheWeek()
        {
            var Start = DateTime.UtcNow;

            var End = Start.AddDays(7);

            return _events.Where(v => v.Start >= Start && v.End <= End).ToList();
        }

        public bool UpdateEvent(Guid idevent,DateTime start, DateTime end, string title, string description, Guid idimage, Guid idspace)
        {
            var uploadingEvent=_events.FirstOrDefault(v=>v.Id== idevent);

            if(uploadingEvent != null)
            {
                if (start != null && start != uploadingEvent.Start) uploadingEvent.Start = start;
                if (end != null && end != uploadingEvent.End) uploadingEvent.End = end;
                if (title != null && title!= uploadingEvent.Title) uploadingEvent.Title = title;
                if (description != null && description!= uploadingEvent.Description) uploadingEvent.Description = description;
                if (idimage != null) uploadingEvent.IdImage = idimage;
                if (idspace != null) uploadingEvent.IdSpace = idspace;
                return true;
            }
            return false;
           
        }
    }
}
