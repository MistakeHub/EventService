using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.EventActiv.Commands.Update
{
    public class UpdateEventCommand:IRequest<ReturnResult>
    {
        public Guid Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Guid IdImage { get; set; }

        public Guid IdSpace { get; set; }
    }
}
