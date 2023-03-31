using EventService.Infrastructure.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.SetFreeTickets;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик заполнения мероприятия бесплатными билетами
/// </summary>
public class SetFreeTicketsCommandRequestHandler : IRequestHandler<SetFreeTicketsCommand, ScResult<string>>
{
    private readonly IBaseEventService _baseEventService;

    /// <summary>
    /// Конструктор
    /// </summary>

    public SetFreeTicketsCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService = baseEventService; }

    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<ScResult<string>> Handle(SetFreeTicketsCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<string>();
        var ticketList = new List<Ticket>();
        var eventDefault = await _baseEventService.GetEventById(request.IdEvent);
        if (eventDefault == null) throw new ScException("Данное мероприятие не существует");
        {
            for (var i = 1; i <= request.Count; i++)
                ticketList.Add(new Ticket { Seat = request.IsAutoGeneratePlaces ? i : null });

            eventDefault.Tickets = ticketList;

            var resultSetFree =
               await _baseEventService.SetFreeTickets(eventDefault);

            if (!resultSetFree) throw new ScException("Билеты не были добавлены");

            returnResult.Result = "Билеты были добавлены";
        }

        return returnResult;
    }
}