
using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Space.Commands.IsExists
{
    public class SpaceExistsCommandQueryHandler : IRequestHandler<SpaceExistsCommand, ScResult<bool>>
    {
        private readonly IBaseSpaceService _baseSpaceService;

        public SpaceExistsCommandQueryHandler(IBaseSpaceService baseSpaceService) { _baseSpaceService=baseSpaceService; }
        public Task<ScResult<bool>> Handle(SpaceExistsCommand request, CancellationToken cancellationToken)
        {
            ScResult<bool> returnResult = new ScResult<bool>();
            var isExists = _baseSpaceService.IsSpaceExists(request.Id);
              if (!isExists) throw new ScException("Изображение не существует");
            returnResult.Result = isExists;
            return Task.FromResult(returnResult);
        }
    }
}
