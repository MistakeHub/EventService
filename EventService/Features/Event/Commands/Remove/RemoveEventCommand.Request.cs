
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Remove;

/// <summary>
/// Команда удаления мероприятия 
/// </summary>
public class RemoveEventCommand:IRequest<ScResult<string>>
{
    /// <summary>
    /// Id мероприятия
    /// </summary>
    public Guid Id { get; set; }
}