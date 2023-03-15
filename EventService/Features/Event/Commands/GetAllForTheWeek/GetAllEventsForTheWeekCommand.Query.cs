using EventService.Helpers;
using MediatR;

namespace EventService.EntityActivities.EventActiv.Commands.GetAllForTheWeek
{
    public class GetAllEventsForTheWeekCommand:IRequest<ReturnResult>
    {
    }
}
