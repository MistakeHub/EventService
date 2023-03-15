using EventService.Helpers;
using EventService.Models.Interfaces;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventService.EntityActivities.SpaceActiv.Commands.IsExists
{
    public class SpaceExistsCommandQueryHandler : IRequestHandler<SpaceExistsCommand, ReturnResult>
    {
        private IBaseSpaceService _baseSpaceService;

        public SpaceExistsCommandQueryHandler(IBaseSpaceService baseSpaceService) { _baseSpaceService=baseSpaceService; }
        public Task<ReturnResult> Handle(SpaceExistsCommand request, CancellationToken cancellationToken)
        {
            ReturnResult returnResult = new ReturnResult();
            var IsExists =  _baseSpaceService.IsSpaceExists(request.Id);
            returnResult.Data = IsExists;
            returnResult.StatusCode = IsExists ? (int)StatuseCode.Success : (int)StatuseCode.BadRequest;
            return Task.FromResult(returnResult);
        }
    }
}
