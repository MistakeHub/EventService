
using EventService.Models.Interfaces;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.Image.Commands.IsExists
{
    // ReSharper disable once UnusedMember.Global Решарпер рекомендует удалить, так как он не используется
    public class ImageExistsCommandQueryHandler:IRequestHandler<ImageExistsCommand, ScResult<bool>>
    {
        private readonly IBaseImageService _baseImageService;

        public ImageExistsCommandQueryHandler(IBaseImageService baseImageService) { _baseImageService = baseImageService; }
        public Task<ScResult<bool>> Handle(ImageExistsCommand request, CancellationToken cancellationToken)
        {
            ScResult<bool> returnResult = new ScResult<bool>();
            var isExists =  _baseImageService.IsImageExists(request.Id);
            if (!isExists) throw new ScException("Изображение не существует");
            returnResult.Result = isExists;
    
            return Task.FromResult(returnResult);
        }
    }
}
