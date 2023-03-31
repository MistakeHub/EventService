
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Create;

/// <summary>
/// Команда для создания мероприятия
/// </summary>
public class CreateEventCommand:IRequest<ScResult<string>>
{
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
    public string Title { get; set; } = null!;

    /// <summary>
    /// Описание мероприятия
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Id изображения мероприятия
    /// </summary>
    public Guid IdImage { get; set; }

    /// <summary>
    /// Id пространства мероприятия
    /// </summary>
    public Guid IdSpace { get; set; }

    /// <summary>
    /// Price
    /// </summary>
    public decimal Price { get; set; }

}