using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Image.Commands.Remove;

/// <summary>
/// Команда для удаления изображения мероприятия
/// </summary>
public class RemoveImageEvent:IRequest<ScResult<string>>
{
    /// <summary>
    /// Id мероприятия
    /// </summary>
    public Guid IdEvent { get; set; }

}