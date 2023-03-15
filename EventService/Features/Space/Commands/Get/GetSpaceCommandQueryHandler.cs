using EventService.Helpers;
using EventService.Models.Interfaces;
using MediatR;

namespace EventService.EntityActivities.SpaceActiv.Commands.Get
{
    public class GetSpaceCommandQueryHandler : IRequestHandler<GetSpaceCommand, ReturnResult>
    {
        private IBaseSpaceService _baseSpaceService;

        public GetSpaceCommandQueryHandler(IBaseSpaceService baseSpaceService) { _baseSpaceService= baseSpaceService; }
        public Task<ReturnResult> Handle(GetSpaceCommand request, CancellationToken cancellationToken)
        {
            ReturnResult returnResult =new ReturnResult();
            var getresult = _baseSpaceService.Get(request.Id);
            returnResult.Data = getresult == null ? "Такого пространства нет!" : getresult;
            returnResult.StatusCode = getresult == null ? (int)StatuseCode.BadRequest : (int)StatuseCode.Success;
            return Task.FromResult(returnResult) ;
        }
    }
}
