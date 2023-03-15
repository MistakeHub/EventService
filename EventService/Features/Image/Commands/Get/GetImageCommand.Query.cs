using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.ImageActiv.Commands.Get
{
    public class GetImageCommand:IRequest<ReturnResult>
    {
        public Guid Id { get; set; }
    }
}
