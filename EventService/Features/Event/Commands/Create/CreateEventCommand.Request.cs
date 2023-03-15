using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.EventActiv.Commands.Create
{
    public class CreateEventCommand:IRequest<ReturnResult>
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid IdImage { get; set; }
        public Guid IdSpace { get; set; }
    }
}
