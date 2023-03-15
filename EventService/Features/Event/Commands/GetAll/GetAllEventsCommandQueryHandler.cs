using AutoMapper;
using EventService.Helpers;
using EventService.Models.Interfaces;
using EventService.Models.ViewModels;
using MediatR;

namespace EventService.EntityActivities.EventActiv.Commands.GetAll
{
    public class GetAllEventsCommandQueryHandler : IRequestHandler<GetAllEventsCommand, ReturnResult>
    {
        private IBaseEventService _baseEventService;
        private IMapper _mapper;
        public GetAllEventsCommandQueryHandler(IBaseEventService baseEventService, IMapper mapper) { _baseEventService= baseEventService; _mapper = mapper; }
        public async Task<ReturnResult> Handle(GetAllEventsCommand request, CancellationToken cancellationToken)
        {
            var returnget = _baseEventService.GetAllEvents();
            var events = _mapper.Map<List<EventViewModel>>(returnget);

            return new ReturnResult() { Data = events, StatusCode = (int)StatuseCode.Success };
        }
    }
}
