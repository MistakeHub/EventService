using EventService.Models.Entities;

namespace EventService.Models.Interfaces
{
    public interface IBaseSpaceService
    {
        public Space Get(Guid id);
        public bool IsSpaceExists(Guid idspace);
    }
}
