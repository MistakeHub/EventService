
using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.SetTickets
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class SetTicketsCommandRequestHandler:IRequestHandler<SetTicketsCommand, ScResult<string>>
    {
        private readonly IBaseEventService _baseEventService;
        
        public SetTicketsCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService=baseEventService;}
        public Task<ScResult<string>> Handle(SetTicketsCommand request, CancellationToken cancellationToken)
        {
            ScResult<string> returnresult = new ScResult<string>();
           var resultsetting= _baseEventService.SetTickets(request.Count, request.IdEvent);
           if (!resultsetting) throw new ScException("Билеты не были добавлены");
           returnresult.Result="Билеты были добавлены" ;
          
           return Task.FromResult(returnresult);
        }
    }
}
