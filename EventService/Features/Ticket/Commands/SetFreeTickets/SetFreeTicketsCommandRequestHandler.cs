using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.Commands.SetFreeTickets;

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
    public Task<ScResult<string>> Handle(SetFreeTicketsCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<string>();
        var resultSetFree = _baseEventService.SetFreeTickets(request.Count, request.IdEvent, request.IsAutoGeneratePlaces);
        if (!resultSetFree) throw new ScException("Билеты не были добавлены");
        returnResult.Result = "Билеты были добавлены";

        return Task.FromResult(returnResult);
    }
}