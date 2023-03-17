
using EventService.Models.Interfaces;
using FluentValidation;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Update
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class UpdateEventCommandRequestHandler : IRequestHandler<UpdateEventCommand, ScResult<string>>
    {
        private readonly IBaseEventService _baseEventService;
        public UpdateEventCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService = baseEventService; }
        public Task<ScResult<string>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            ScResult<string> returnresult = new ScResult<string>();

            UpdateEventCommandValidation validator = new UpdateEventCommandValidation();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid) throw new ScException(new ValidationException(validationResult.Errors), "ValidaitonException");


            var resultupdate = _baseEventService.UpdateEvent(request.Id, request.Start, request.End, request.Title, request.Description, request.IdImage, request.IdSpace);
            if (!resultupdate) throw new ScException("Мероприятие не было обновлено");
            returnresult.Result=  "Мероприятие было обновлено";
  
            return Task.FromResult(returnresult);
        }
    }
}
