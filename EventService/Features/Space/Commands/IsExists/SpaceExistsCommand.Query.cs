using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.SpaceActiv.Commands.IsExists
{
    public class SpaceExistsCommand:IRequest<ReturnResult>
    {
        public Guid Id { get; set; }
    }
}
