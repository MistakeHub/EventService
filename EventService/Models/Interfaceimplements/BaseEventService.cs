using EventService.Models.Entities;
using EventService.Models.Interfaces;


namespace EventService.Models.Interfaceimplements
{
    public class BaseEventService:IBaseEventService
    {
        private readonly List<Event> _events;
        
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
            var start = DateTime.UtcNow;

            var end = start.AddDays(7);

            return _events.Where(v => v.Start >= start && v.End <= end).ToList();
        }

        public bool SetTickets(int count, Guid idevent)
        {
           var eventDefault= _events.FirstOrDefault(v => v.Id == idevent);
           if (eventDefault != null)
           {
               for (int i = 1; i <= count; i++) eventDefault.Tickets.Add(new Ticket(){Place = i});
               return true;
           }

           return false;
        }

        public Ticket IssueTicket(Guid idevent, Guid idowner, int place)
        {
            var eventDefault = _events.FirstOrDefault(v => v.Id == idevent);
            if (eventDefault != null)
            {
                var desireTicket = eventDefault.Tickets.FirstOrDefault(v => v.Place == place);
                if (desireTicket.IdOwner == null)
                {
                    desireTicket.IdOwner = idowner;
                    return desireTicket;
                }
           
            }

            return null;
        }

        public bool HaveATicket(Guid idevent, Guid idowner)
        {
            var eventDefault = _events.FirstOrDefault(v => v.Id == idevent);
            if (eventDefault != null)
            {
               return eventDefault.Tickets.Any(v => v.IdOwner == idowner);
            }

            return false;
        }

        public bool UpdateEvent(Guid idevent,DateTime? start, DateTime? end, string? title, string? description, Guid? idimage, Guid? idspace)
        {
            var uploadingEvent=_events.FirstOrDefault(v=>v.Id== idevent);

            if(uploadingEvent != null)
            {
                if (start != null && start != uploadingEvent.Start) uploadingEvent.Start = start;
                if (end != null && end != uploadingEvent.End) uploadingEvent.End = end;
                if (string.IsNullOrEmpty( title) && title!= uploadingEvent.Title) uploadingEvent.Title = title;
                if (string.IsNullOrEmpty( description) && description!= uploadingEvent.Description) uploadingEvent.Description = description;
                if (idimage != null) uploadingEvent.IdImage = idimage;
                if (idspace != null) uploadingEvent.IdSpace = idspace;
                return true;
            }
            return false;
           
        }
    }
}
