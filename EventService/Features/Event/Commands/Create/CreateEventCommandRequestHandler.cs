using EventService.Helpers;
using EventService.Models.Interfaces;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventService.EntityActivities.EventActiv.Commands.Create
{
    public class CreateEventCommandRequestHandler : IRequestHandler<CreateEventCommand, ReturnResult>
    {
        private IBaseEventService _baseEventService;

        public CreateEventCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService= baseEventService; }
        public async Task<ReturnResult> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            ReturnResult returnresult = new ReturnResult();
            CreateEventCommandValidation validator= new CreateEventCommandValidation();
           var error =validator.Validate(request).Errors.FirstOrDefault();
            if (error !=null)
            {
               
                returnresult.Data = error.ErrorMessage;
                returnresult.StatusCode = (int)StatuseCode.BadRequest;
                return returnresult;
            }
            var resultofadd = _baseEventService.CreateEvent(request.Start, request.End, request.Title, request.Description, request.IdImage, request.IdSpace);

            returnresult.Data= resultofadd? "Мероприятие было создано":"Мероприятие не было обновлено" ;
            returnresult.StatusCode = resultofadd ? (int)StatuseCode.Created : (int)StatuseCode.Unprocessable;
            return returnresult;

           

        }
    }
}
