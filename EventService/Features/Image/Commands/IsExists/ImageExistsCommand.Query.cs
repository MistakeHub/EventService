using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.ImageActiv.Commands.IsExists
{
    public class ImageExistsCommand:IRequest<ReturnResult>
    {
        public Guid Id { get; set; }
    }
}
