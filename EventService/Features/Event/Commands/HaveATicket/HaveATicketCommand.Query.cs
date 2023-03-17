
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.HaveATicket
{
    public class HaveATicketCommand:IRequest<ScResult<bool>>
    {
        public Guid IdOwner { get; set; }

        public Guid IdEvent { get; set; }
    }
}
