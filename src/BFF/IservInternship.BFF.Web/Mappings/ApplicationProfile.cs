using AutoMapper;
using IservInternship.BFF.Web.Models;
using IservInternship.Domain.Application.Entities;

namespace IservInternship.BFF.Web.Mappings;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<ApplicationEntity, ApplicationDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.AboutMe, opt => opt.MapFrom(s => s.AboutMe))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
            ;

        CreateMap<CreateApplicationRequest, ApplicationEntity>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.AboutMe, opt => opt.MapFrom(s => s.AboutMe))
            ;

        CreateMap<PatchApplicationRequest, ApplicationEntity>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.AboutMe, opt => opt.MapFrom(s => s.AboutMe))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
            ;
    }
}
