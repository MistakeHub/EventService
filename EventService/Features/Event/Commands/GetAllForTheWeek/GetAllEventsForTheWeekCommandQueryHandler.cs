using AutoMapper;

using EventService.Models.Interfaces;
using EventService.Models.ViewModels;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.GetAllForTheWeek
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class GetAllEventsForTheWeekCommandQueryHandler : IRequestHandler<GetAllEventsForTheWeekCommand, ScResult<List<EventViewModel>>>
    {
        private readonly IBaseEventService _baseEventService;
        private readonly IMapper _mapper;
        public GetAllEventsForTheWeekCommandQueryHandler(IBaseEventService baseEventService, IMapper mapper) { _baseEventService=baseEventService; _mapper = mapper; }
        public Task<ScResult<List<EventViewModel>>> Handle(GetAllEventsForTheWeekCommand request, CancellationToken cancellationToken)
        {
            var returnget = _baseEventService.GetAllEventsForTheWeek();
            var events = _mapper.Map<List<EventViewModel>>(returnget);

            return Task.FromResult(new ScResult<List<EventViewModel>>(){Result = events});
        }
    }
}
