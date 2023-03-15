using EventService.Models.Entities;
using EventService.Models.Interfaces;

namespace EventService.Models.Interfaceimplements
{
    public class BaseSpaceService:IBaseSpaceService
    {
        private List<Space> _spaces;

        public BaseSpaceService() { _spaces = new List<Space>() { new Space() { Id = new Guid("7febf16f-651b-43b0-a5e3-0da8da49e90d"), Name = "Пространство 1" } };  }

        public Space Get(Guid id)
        {
         return _spaces.FirstOrDefault(v=>v.Id==id);
        }

        public bool IsSpaceExists(Guid idspace)
        {
           return _spaces.Any(v=>v.Id==idspace);
        }

       
    }
}
