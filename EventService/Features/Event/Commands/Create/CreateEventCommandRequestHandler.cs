
using EventService.Models.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Event.Commands.Create
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class CreateEventCommandRequestHandler : IRequestHandler<CreateEventCommand, ScResult<string>>
    {
        private readonly IBaseEventService _baseEventService;

        public CreateEventCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService= baseEventService; }
        public Task<ScResult<string>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
          
            CreateEventCommandValidation validator = new CreateEventCommandValidation();
            ValidationResult validationResult = null;
        
                validationResult = validator.Validate(request);
                if (!validationResult.IsValid) throw new ScException(new ValidationException(validationResult.Errors), "ValidationException");
             


                var resultofadd = _baseEventService.CreateEvent(request.Start, request.End, request.Title,
                    request.Description, request.IdImage, request.IdSpace);
                if (!resultofadd) throw new ScException("Мероприятие не было создано");

            ScResult<string> returnresult  =new ScResult<string>("Мероприятие было создано")  ;
                
            return Task.FromResult(returnresult);

           

        }
    }
}
