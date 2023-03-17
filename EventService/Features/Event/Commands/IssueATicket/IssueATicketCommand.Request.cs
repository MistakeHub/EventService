
using EventService.Models.Entities;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.IssueATicket
{
    public class IssueATicketCommand:IRequest<ScResult<Ticket>>
    {
        public Guid IdEvent { get; set; }

        public Guid IdOwner { get; set; }

        public int Place { get; set; }
    }
}
