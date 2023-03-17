using AutoMapper;

using EventService.Models.Interfaces;
using EventService.Models.ViewModels;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.GetAll
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class GetAllEventsCommandQueryHandler : IRequestHandler<GetAllEventsCommand, ScResult<List<EventViewModel>>>
    {
        private readonly IBaseEventService _baseEventService;
        private readonly IMapper _mapper;
        public GetAllEventsCommandQueryHandler(IBaseEventService baseEventService, IMapper mapper) { _baseEventService= baseEventService; _mapper = mapper; }
        public Task<ScResult<List<EventViewModel>>> Handle(GetAllEventsCommand request, CancellationToken cancellationToken)
        {
            var returnget = _baseEventService.GetAllEvents();
            var events = _mapper.Map<List<EventViewModel>>(returnget);

            return Task.FromResult(new ScResult<List<EventViewModel>>(){Result = events});
        }
    }
}
