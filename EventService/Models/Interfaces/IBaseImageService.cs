using EventService.Models.Entities;

namespace EventService.Models.Interfaces
{
    public interface IBaseImageService
    {
        public Image Get(Guid id);
        public bool IsImageExists(Guid idimage);

    }
}
