
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Update;

/// <summary>
/// Команда обновления мероприятия
/// </summary>
public class UpdateEventCommand:IRequest<ScResult<string>>
{
    /// <summary>
    ///Id мероприятия
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Дата начала мероприятия
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Дата окончания мероприятия
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Название мероприятия
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Описание мероприятия
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Id изображения мероприятия
    /// </summary>
    public Guid? IdImage { get; set; }

    /// <summary>
    /// Id пространства мероприятия
    /// </summary>
    public Guid? IdSpace { get; set; }
}