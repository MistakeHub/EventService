
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Space.Commands.IsExists
{
    public class SpaceExistsCommand:IRequest<ScResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
