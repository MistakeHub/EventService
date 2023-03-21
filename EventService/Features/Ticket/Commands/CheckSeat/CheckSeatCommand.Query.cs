using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.Commands.CheckSeat;

/// <summary>
/// Команда проверки наличия места
/// </summary>
public class CheckSeatCommand:IRequest<ScResult<int?>>
{
    /// <summary>
    /// Id мероприятия
    /// </summary>
    public Guid IdEvent { get; set; }

    /// <summary>
    /// Id места
    /// </summary>
    public Guid IdTicket { get; set; }
}