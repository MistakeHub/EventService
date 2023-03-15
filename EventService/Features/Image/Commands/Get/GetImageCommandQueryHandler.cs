using EventService.EntityActivities.SpaceActiv.Commands.Get;
using EventService.Helpers;
using EventService.Models.Interfaceimplements;
using EventService.Models.Interfaces;
using MediatR;

namespace EventService.EntityActivities.ImageActiv.Commands.Get
{
    public class GetImageCommandQueryHandler:IRequestHandler<GetImageCommand, ReturnResult>
    {

        private IBaseImageService _baseImageService;

        public GetImageCommandQueryHandler(IBaseImageService baseImageService) { _baseImageService = baseImageService; }
        public Task<ReturnResult> Handle(GetImageCommand request, CancellationToken cancellationToken)
        {
            ReturnResult returnResult = new ReturnResult();
            var getresult = _baseImageService.Get(request.Id);
            returnResult.Data = getresult == null ? "Такого изображения нет!" : getresult;
            returnResult.StatusCode = getresult == null ? (int)StatuseCode.BadRequest : (int)StatuseCode.Success;
            return Task.FromResult(returnResult);
        }
    }
}
