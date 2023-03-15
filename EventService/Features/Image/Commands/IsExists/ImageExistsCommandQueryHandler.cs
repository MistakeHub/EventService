using EventService.EntityActivities.SpaceActiv.Commands.IsExists;
using EventService.Helpers;
using EventService.Models.Interfaces;
using MediatR;

namespace EventService.EntityActivities.ImageActiv.Commands.IsExists
{
    public class ImageExistsCommandQueryHandler:IRequestHandler<ImageExistsCommand,ReturnResult>
    {
        private IBaseImageService _baseImageService;

        public ImageExistsCommandQueryHandler(IBaseImageService baseImageService) { _baseImageService = baseImageService; }
        public Task<ReturnResult> Handle(ImageExistsCommand request, CancellationToken cancellationToken)
        {
             ReturnResult returnResult = new ReturnResult();
            var IsExists =  _baseImageService.IsImageExists(request.Id);
            returnResult.Data = IsExists;
            returnResult.StatusCode = IsExists ? (int)StatuseCode.Success : (int)StatuseCode.BadRequest;
            return Task.FromResult(returnResult);
        }
    }
}
