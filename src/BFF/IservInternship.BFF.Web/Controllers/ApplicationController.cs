using Asp.Versioning;
using AutoMapper;
using IservInternship.Application.Services;
using IservInternship.BFF.Web.Models;
using IservInternship.Commons.Exceptions;
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
    public async Task<ApplicationDetailsDto> GetApplicationById(Guid id)
    {
        var entity = await applicationService.GetApplicationByIdAsync(id);
        return mapper.Map<ApplicationDetailsDto>(entity);
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ApplicationDetailsDto> PatchApplicationStatus(
        Guid id, [FromBody] Status status)
    {
        var entity = await applicationService.UpdateApplicationStatusAsync(id, status);
        return mapper.Map<ApplicationDetailsDto>(entity);
    }

    [HttpPatch("{id:guid}/solution_status")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<ApplicationDetailsDto> PatchApplicationSolutionStatus(
        Guid id, [FromBody] Status status)
    {
        var entity = await applicationService.UpdateApplicationSolutionStatusAsync(id, status);
        return mapper.Map<ApplicationDetailsDto>(entity);
    }

    [HttpGet]
    public async Task<ApplicationDetailsDto> GetApplicationByUserUid()
    {
        var userUid = Guid.Parse(GetRetrieveUserIdentificator()!);
        var entity = await applicationService.GetApplicationByUserUidAsync(userUid);
        return mapper.Map<ApplicationDetailsDto>(entity);
    }

    [HttpPost]
    public async Task<ApplicationDetailsDto> AddApplication([FromBody] CreateApplicationRequest request)
    {
        var userUid = Guid.Parse(GetRetrieveUserIdentificator()!);
        var newEntity = mapper.Map<ApplicationEntity>(request);
        newEntity.UserUid = userUid;

        var entity = await applicationService.AddApplicationAsync(newEntity);
        return mapper.Map<ApplicationDetailsDto>(entity);
    }

    [HttpPatch("{id:guid}")]
    public async Task<ApplicationDetailsDto> PatchApplication(Guid id, [FromBody] PatchApplicationRequest request)
    {
        var entity = await applicationService.UpdateApplicationAsync(id, mapper.Map<ApplicationEntity>(request));
        return mapper.Map<ApplicationDetailsDto>(entity);
    }

    [HttpPatch("{id:guid}/solution")]
    public async Task<ApplicationDetailsDto> PatchApplicationSolution(Guid id, [FromBody] string solution)
    {
        var entity = await applicationService.UpdateApplicationSolutionAsync(id, solution);
        return mapper.Map<ApplicationDetailsDto>(entity);
    }

    [HttpPatch("{id:guid}/answer")]
    public async Task<ApplicationDetailsDto> PatchApplicationAnswer(Guid id, [FromBody] string answer)
    {
        var entity = await applicationService.UpdateApplicationAnswerAsync(id, answer);
        return mapper.Map<ApplicationDetailsDto>(entity);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ApplicationDetailsDto> DeleteApplication(Guid id)
    {
        var entity = await applicationService.RemoveApplicationAsync(id);
        return mapper.Map<ApplicationDetailsDto>(entity);
    }
}
