
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Space.Commands.Remove;

/// <summary>
/// Команда для удаления пространства мероприятия
/// </summary>
public class RemoveSpaceEvent:IRequest<ScResult<string>>
{
    /// <summary>
    /// Id мероприятия 
    /// </summary>
    public Guid IdEvent { get; set; }
}