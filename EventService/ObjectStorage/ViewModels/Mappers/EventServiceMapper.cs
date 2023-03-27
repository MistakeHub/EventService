using AutoMapper;
using EventService.Models.Entities;

namespace EventService.ObjectStorage.ViewModels.Mappers;

// ReSharper disable once UnusedMember.Global Решарпер предлагает удалить класс конфигурации маппера, т.к он 'не используется'
/// <summary>
/// Класс конфигурации маппера 
/// </summary>
public class EventServiceMapper : Profile
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    public EventServiceMapper()
    {
        CreateMap<Event, EventViewModel>().ForMember(opt => opt.Id, des => des.MapFrom(v => v.Id))
            .ForMember(opt => opt.Title, des => des.MapFrom(v => v.Title))
            .ForMember(opt => opt.Description, des => des.MapFrom(v => v.Description))
            .ForMember(opt => opt.Start, des => des.MapFrom(v => v.Start))
            .ForMember(opt => opt.IdImage, des => des.MapFrom(v => v.IdImage))
            .ForMember(opt => opt.IdSpace, des => des.MapFrom(v => v.IdSpace))
            .ForMember(opt => opt.IsTicketsAvailable, des => des.MapFrom(v => v.IsTicketsAvailable))
            .ForMember(opt => opt.HaveTicketsSeats, des => des.MapFrom(v => v.HaveTicketsSeats)).
            ForMember(opt => opt.Price, des => des.MapFrom(v => v.Price)); 



    }
}