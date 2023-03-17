
using EventService.Models.Interfaces;
using FluentValidation;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Remove
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class RemoveEventCommandRequestHandler : IRequestHandler<RemoveEventCommand, ScResult<string>>
    {
        private readonly IBaseEventService _baseEventService;

        public RemoveEventCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService= baseEventService; }
        public Task<ScResult<string>> Handle(RemoveEventCommand request, CancellationToken cancellationToken)
        {
            ScResult<string> returnresult = new ScResult<string>();
            RemoveEventCommandValidation validation = new RemoveEventCommandValidation();
            var errors = validation.Validate(request).Errors;
            if (errors != null) throw new ScException(new ValidationException(errors),"Мероприятие не было удалено");
            var resuldelete = _baseEventService.DeleteEvent(request.Id);

         
            if (!resuldelete) throw new ScException("Мероприятие не было удалено");
            returnresult.Result= "Мероприятие было удалено";

            return Task.FromResult(returnresult);
        }
    }
}
