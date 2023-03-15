using AutoMapper;
using EventService.Helpers;
using EventService.Models.Interfaces;
using EventService.Models.ViewModels;
using MediatR;

namespace EventService.EntityActivities.EventActiv.Commands.GetAllForTheWeek
{
    public class GetAllEventsForTheWeekCommandQueryHandler : IRequestHandler<GetAllEventsForTheWeekCommand, ReturnResult>
    {
        private IBaseEventService _baseEventService;
        private IMapper _mapper;
        public GetAllEventsForTheWeekCommandQueryHandler(IBaseEventService baseEventService, IMapper mapper) { _baseEventService=baseEventService; _mapper = mapper; }
        public async Task<ReturnResult> Handle(GetAllEventsForTheWeekCommand request, CancellationToken cancellationToken)
        {
            var returnget = _baseEventService.GetAllEventsForTheWeek();
            var events = _mapper.Map<List<EventViewModel>>(returnget);

            return new ReturnResult() { Data = events, StatusCode = (int)StatuseCode.Success };
        }
    }
}
