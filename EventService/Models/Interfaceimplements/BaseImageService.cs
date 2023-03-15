using EventService.Models.Entities;
using EventService.Models.Interfaces;


namespace EventService.Models.Interfaceimplements
{
    public class BaseImageService:IBaseImageService
    {
        private List<Image> _images;
        public BaseImageService() { _images = new List<Image>() { new Image { Id = new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d"), FilePath="GreateSpace.jpg" } }; }

        public Image Get(Guid id)
        {
            return _images.FirstOrDefault(v => v.Id == id);
        }

        public bool IsImageExists(Guid idimage)
        {
            return _images.Any(v => v.Id == idimage);
        }
    }
}
