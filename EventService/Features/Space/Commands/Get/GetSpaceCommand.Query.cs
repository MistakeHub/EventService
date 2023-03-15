using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.SpaceActiv.Commands.Get
{
    public class GetSpaceCommand:IRequest<ReturnResult>
    {
        public Guid Id { get; set; }
    }
}
