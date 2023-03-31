using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.GetAllForTheWeek;

/// <summary>
/// Команда получения всех мероприятий на неделю
/// </summary>
public class GetAllEventsForTheWeekCommand:IRequest<ScResult<List<EventViewModel>>>
{
}