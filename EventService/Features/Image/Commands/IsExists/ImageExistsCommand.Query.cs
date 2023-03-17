
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Image.Commands.IsExists
{
    public class ImageExistsCommand:IRequest<ScResult<bool>>
    {
        public Guid Id { get; set; }
    }
}
