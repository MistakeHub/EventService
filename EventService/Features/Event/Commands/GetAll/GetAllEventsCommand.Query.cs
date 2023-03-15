using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.EventActiv.Commands.GetAll
{
    public class GetAllEventsCommand:IRequest<ReturnResult>
    {
    }
}
