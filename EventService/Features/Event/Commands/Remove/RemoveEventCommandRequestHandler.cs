using EventService.Helpers;
using EventService.Models.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace EventService.EntityActivities.EventActiv.Commands.Remove
{
    public class RemoveEventCommandRequestHandler : IRequestHandler<RemoveEventCommand, ReturnResult>
    {
        private IBaseEventService _baseEventService;

        public RemoveEventCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService= baseEventService; }
        public async Task<ReturnResult> Handle(RemoveEventCommand request, CancellationToken cancellationToken)
        {
            ReturnResult returnresult = new ReturnResult();

            var resuldelete = _baseEventService.DeleteEvent(request.Id);
            RemoveEventCommandValidation validator= new RemoveEventCommandValidation();
            var error = validator.Validate(request).Errors.FirstOrDefault();
            if (error != null)
            {

                returnresult.Data = error.ErrorMessage;
                returnresult.StatusCode = (int)StatuseCode.BadRequest;
                return returnresult;
            }
            returnresult.Data = resuldelete ? "Мероприятие было удалено" : "Мероприятие не было удалено";
            returnresult.StatusCode = resuldelete ? (int)StatuseCode.Success : (int)StatuseCode.Unprocessable;
            return returnresult;
        }
    }
}
