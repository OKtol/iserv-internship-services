using AutoMapper;
using IservInternship.BFF.Web.Models;
using IservInternship.Domain.Application.Entities;

namespace IservInternship.BFF.Web.Mappings;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<ApplicationEntity, ApplicationDetailsDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.JobTitle, opt => opt.MapFrom(s => s.Job.Title))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.AboutMe, opt => opt.MapFrom(s => s.AboutMe))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
            .ForMember(d => d.Solution, opt => opt.MapFrom(s => s.Solution))
            .ForMember(d => d.VerificationStatus, opt => opt.MapFrom(s => s.VerificationStatus))
            .ForMember(d => d.Answer, opt => opt.MapFrom(s => s.Answer))
            .ForMember(d => d.SolutionStatus, opt => opt.MapFrom(s => s.SolutionStatus))
            ;

        CreateMap<ApplicationEntity, ApplicationDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.JobTitle, opt => opt.MapFrom(s => s.Job.Title))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status))
            .ForMember(d => d.VerificationStatus, opt => opt.MapFrom(s => s.VerificationStatus))
            .ForMember(d => d.SolutionStatus, opt => opt.MapFrom(s => s.SolutionStatus))
            ;

        CreateMap<CreateApplicationRequest, ApplicationEntity>()
            .ForMember(d => d.JobId, opt => opt.MapFrom(s => s.JobId))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.Email))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.AboutMe, opt => opt.MapFrom(s => s.AboutMe))
            ;

        CreateMap<PatchApplicationRequest, ApplicationEntity>()
            .ForMember(d => d.JobId, opt => opt.MapFrom(s => s.JobId))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName))
            .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.AboutMe, opt => opt.MapFrom(s => s.AboutMe))
            ;

        CreateMap<JobEntity, JobDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
            ;

        CreateMap<JobEntity, JobDetailsDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Title, opt => opt.MapFrom(s => s.Title))
            .ForMember(d => d.IsVisible, opt => opt.MapFrom(s => s.IsVisible))
            ;
    }
}
