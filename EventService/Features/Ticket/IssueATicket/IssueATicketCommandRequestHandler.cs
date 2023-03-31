
using EventService.Features.Ticket.IssueATicket.notifications.CanclellationPayment;
using EventService.Features.Ticket.IssueATicket.notifications.ConfirmationPayment;
using EventService.ObjectStorage.HttpService;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;
using EventService.Infrastructure.Interfaces;

namespace EventService.Features.Ticket.IssueATicket;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить хэндлер команды, т.к он явно нигде не используется
/// <summary>
/// Класс обработчик команды выдачи билета на мероприятие
/// </summary>
public class IssueATicketCommandRequestHandler : IRequestHandler<IssueATicketCommand, ScResult<Ticket>>
{
    private readonly IBaseEventService _baseEventService;

    private readonly HttpServiceClient _httpServiceClient;

    private readonly IMediator _mediator;
    /// <summary>
    /// Конструктор
    /// </summary>
    public IssueATicketCommandRequestHandler(IBaseEventService baseEventService, HttpServiceClient httpServiceClient, IMediator mediator)
    {
        _baseEventService = baseEventService;
        _httpServiceClient = httpServiceClient;
        _mediator = mediator;

    }
    /// <summary>
    /// Обработчик
    /// </summary>
    public async Task<ScResult<Ticket>> Handle(IssueATicketCommand request, CancellationToken cancellationToken)
    {
        var returnResult = new ScResult<Ticket>();
        var eventDefault = await _baseEventService.GetEventById(request.IdEvent);
        if (eventDefault == null) throw new ScException("Данное мероприятие не существует");
        {
            var desireTicket = eventDefault.Tickets.First(v => v.IdOwner == null);
            if (!eventDefault.IsTicketsAvailable) throw new ScException("Нет свободных билетов");
            if (request.Price > 0)
            {
                var setPayment = await _httpServiceClient.SendRequest<ScResult<Guid>>("payment", $"?description=Билет на мероприятие:{request.IdEvent} на сумму:{request.Price}",
                        "POST", null!, request.Authorization!);
                if (setPayment.Result == Guid.Empty) throw new ScException("Невозможно создать платёжную операцию");

                desireTicket.IdOwner = request.IdOwner;

                var ticket = await _baseEventService.IssueTicket(eventDefault);
                if (!ticket)
                {
                    await _mediator.Publish(new CancellationPaymentEvent{ IdPayment = setPayment.Result }, cancellationToken);
                    throw new ScException("Невозможно выдать билет");
                }

                await _mediator.Publish(new ConfirmationPaymentEvent { IdPayment = setPayment.Result }, cancellationToken);

                returnResult.Result = desireTicket;
            }
            else
            {
                var ticket = await _baseEventService.IssueTicket(eventDefault);
                if (!ticket) throw new ScException("Невозможно выдать билет");
                returnResult.Result = desireTicket;
            }
            return returnResult;
        }

    }
}