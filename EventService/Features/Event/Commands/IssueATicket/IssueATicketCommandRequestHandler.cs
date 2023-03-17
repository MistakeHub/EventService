
using EventService.Models.Entities;
using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.IssueATicket
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class IssueATicketCommandRequestHandler:IRequestHandler<IssueATicketCommand, ScResult<Ticket>>
    {
        private readonly IBaseEventService _baseEventService;

        public IssueATicketCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService = baseEventService; }
        public Task<ScResult<Ticket>> Handle(IssueATicketCommand request, CancellationToken cancellationToken)
        {
            ScResult<Ticket> returnresut = new ScResult<Ticket>();

           var ticket= _baseEventService.IssueTicket(request.IdEvent, request.IdOwner, request.Place);
           if (ticket == null) throw new ScException("Невозможно выдать билет");
           returnresut.Result = ticket;
          
           return Task.FromResult(returnresut);
        }
    }
}
