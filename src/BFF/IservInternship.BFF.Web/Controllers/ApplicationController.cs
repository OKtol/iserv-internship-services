using Asp.Versioning;
using AutoMapper;
using IservInternship.Application.Services;
using IservInternship.BFF.Web.Models;
using IservInternship.Domain.Application.Entities;
using IservInternship.Domain.Application.Enums;
using IservInternship.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IservInternship.BFF.Web.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ApplicationController(
    IMapper mapper,
    ApplicationService applicationService,
    IUserContextService userContextService) : UserContextController(userContextService)
{
    [HttpGet("all")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IEnumerable<ApplicationDto>> GetAllApplications()
    {
        var entities = await applicationService.GetApplicationsAsync();
        return mapper.Map<ApplicationDto[]>(entities);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ApplicationDto> GetApplicationById(Guid id)
    {
        var entity = await applicationService.GetApplicationByIdAsync(id);
        return mapper.Map<ApplicationDto>(entity);
    }

    [HttpGet]
    public async Task<ApplicationDto> GetApplicationByUserUid()
    {
        var userUid = Guid.Parse(GetRetrieveUserIdentificator()!);
        var entity = await applicationService.GetApplicationByUserUidAsync(userUid);
        return mapper.Map<ApplicationDto>(entity);
    }

    [HttpPost]
    public async Task<ApplicationDto> AddApplication([FromBody] CreateApplicationRequest request)
    {
        var userUid = Guid.Parse(GetRetrieveUserIdentificator()!);
        var newEntity = new ApplicationEntity
        {
            UserUid = userUid,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            AboutMe = request.AboutMe,
            Status = Status.Considered
        };
        var entity = await applicationService.AddApplicationAsync(newEntity);
        return mapper.Map<ApplicationDto>(entity);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ApplicationDto> PatchApplication(Guid id, [FromBody] PatchApplicationRequest request)
    {
        var entity = await applicationService.UpdateApplicationAsync(id, mapper.Map<ApplicationEntity>(request));
        return mapper.Map<ApplicationDto>(entity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ApplicationDto> DeleteApplication(Guid id)
    {
        var entity = await applicationService.RemoveApplicationAsync(id);
        return mapper.Map<ApplicationDto>(entity);
    }
}
