using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.EventActiv.Commands.Remove
{
    public class RemoveEventCommand:IRequest<ReturnResult>
    {
        public Guid Id { get; set; }
    }
}
