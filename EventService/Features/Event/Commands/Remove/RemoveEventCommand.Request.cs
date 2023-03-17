
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Remove
{
    public class RemoveEventCommand:IRequest<ScResult<string>>
    {
        public Guid Id { get; set; }
    }
}
