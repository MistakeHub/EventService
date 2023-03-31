using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.GetAll;

/// <summary>
/// Команда для получения всех мероприятий
/// </summary>
public class GetAllEventsCommand:IRequest<ScResult<List<EventViewModel>>>
{
}