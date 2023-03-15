using EventService.EntityActivities.EventActiv.Commands.Remove;
using EventService.Helpers;
using EventService.Models.Interfaces;
using MediatR;

namespace EventService.EntityActivities.EventActiv.Commands.Update
{
    public class UpdateEventCommandRequestHandler : IRequestHandler<UpdateEventCommand, ReturnResult>
    {
        private IBaseEventService _baseEventService;
        public UpdateEventCommandRequestHandler(IBaseEventService baseEventService) { _baseEventService = baseEventService; }
        public async Task<ReturnResult> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            ReturnResult returnresult=new ReturnResult();

            UpdateEventCommandValidation validator = new UpdateEventCommandValidation();
            var error = validator.Validate(request).Errors.FirstOrDefault();
            if (error != null)
            {
                returnresult.Data = error.ErrorMessage;
                returnresult.StatusCode = (int)StatuseCode.BadRequest;
                return returnresult;
            }
            var resultupdate = _baseEventService.UpdateEvent(request.Id, request.Start, request.End, request.Title, request.Description, request.IdImage, request.IdSpace);
            returnresult.Data = resultupdate ? "Мероприятие было обновлено" : "Мероприятие не было создано";
            returnresult.StatusCode = resultupdate ? (int)StatuseCode.Success : (int)StatuseCode.Unprocessable;
            return returnresult;
        }
    }
}
