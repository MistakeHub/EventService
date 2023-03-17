
using EventService.Models.Interfaces;
using MediatR;

using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.HaveATicket
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class HaveATicketCommandQueryHandler:IRequestHandler<HaveATicketCommand, ScResult<bool>>
    {
        private readonly IBaseEventService _baseEventService;

        public HaveATicketCommandQueryHandler(IBaseEventService baseEventService) { _baseEventService = baseEventService; }
        public Task<ScResult<bool>> Handle(HaveATicketCommand request, CancellationToken cancellationToken)
        {
            ScResult<bool> returnResult = new ScResult<bool>();

           var haveaticket = _baseEventService.HaveATicket(request.IdEvent, request.IdOwner);
        

           returnResult.Result=haveaticket;
        
           return Task.FromResult(returnResult);
        }
    }
}
