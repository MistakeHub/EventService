
using EventService.Models.ViewModels;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.GetAllForTheWeek
{
    public class GetAllEventsForTheWeekCommand:IRequest<ScResult<List<EventViewModel>>>
    {
    }
}
