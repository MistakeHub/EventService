using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.SetTickets
{
    public class SetTicketsCommand:IRequest<ScResult<string>>
    {
        public Guid IdEvent { get; set; }
        public int Count { get; set; }

    }
}
