using AutoMapper;
using EventService.Models.Entities;

namespace EventService.Models.ViewModels.Mappers
{
    public class EventServiceMapper:Profile
    {
        public EventServiceMapper() {
            CreateMap<Event, EventViewModel>().ForMember(opt => opt.Id, des => des.MapFrom(v => v.Id)).
            ForMember(opt=>opt.Title, des=>des.MapFrom(v=>v.Title)).
            ForMember(opt => opt.Description, des => des.MapFrom(v => v.Description)).
           ForMember(opt => opt.Start, des => des.MapFrom(v => v.Start)).
           ForMember(opt => opt.IdImage, des => des.MapFrom(v => v.IdImage)).
            ForMember(opt => opt.IdSpace, des => des.MapFrom(v => v.IdSpace));

            

        }
    }
}
