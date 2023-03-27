
using EventService.Models.Interfaces;
using EventService.ObjectStorage.HttpService;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Ticket.Commands.IssueATicket;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды выдачи билета на мероприятие
/// </summary>
public class IssueATicketCommandRequestHandler : IRequestHandler<IssueATicketCommand, ScResult<Models.Entities.Ticket>>
{
    private readonly IBaseEventService _baseEventService;

    private readonly HttpServiceClient _httpServiceClient;
    /// <summary>
    /// Конструктор
    /// </summary>
    public IssueATicketCommandRequestHandler(IBaseEventService baseEventService, HttpServiceClient httpServiceClient) { _baseEventService = baseEventService;
        _httpServiceClient = httpServiceClient;
    }
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<ScResult<Models.Entities.Ticket>> Handle(IssueATicketCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<Models.Entities.Ticket>();
        if (request.Price > 0)
        {
            var setPayment =
                await _httpServiceClient.SendRequest<ScResult<Guid>>("payment", $"?description=Билет на мероприятие:{request.IdEvent} на сумму:{request.Price}",
                    "POST", null!, request.Authorization!);
            if (setPayment.Result == Guid.Empty) throw new ScException("Невозможно создать платёжную операцию");
            var ticket = _baseEventService.IssueTicket(request.IdEvent, request.IdOwner);
            if (ticket == null)
            {
                await _httpServiceClient.SendRequest<ScResult<Guid>>("payment", $"/cancellation/{setPayment.Result}", "Put");
                throw new ScException("Невозможно выдать билет");
            }
            await _httpServiceClient.SendRequest<ScResult<Guid>>("payment", $"/confirmation/{setPayment.Result}", "Put");
            returnResult.Result = ticket;
        }
        else
        {
            var ticket = _baseEventService.IssueTicket(request.IdEvent, request.IdOwner);
            returnResult.Result = ticket;
        }
        return returnResult;
    }
}