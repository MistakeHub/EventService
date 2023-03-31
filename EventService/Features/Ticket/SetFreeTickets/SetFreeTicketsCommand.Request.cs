using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.SetFreeTickets;

/// <summary>
/// Команда заполнения мероприятия бесплатными билетами
/// </summary>
public class SetFreeTicketsCommand : IRequest<ScResult<string>>
{
    /// <summary>
    /// Id мероприятия
    /// </summary>
    public Guid IdEvent { get; set; }
    /// <summary>
    /// Количества билетов
    /// </summary>
    public int Count { get; set; }
    /// <summary>
    /// Поле указывающая: требуется ли автогенерация мест для билетов
    /// </summary>
    public bool IsAutoGeneratePlaces { get; set; }

}