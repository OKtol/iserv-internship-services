using Asp.Versioning;
using AutoMapper;
using IservInternship.Application.Services;
using IservInternship.BFF.Web.Models;
using IservInternship.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IservInternship.BFF.Web.Controllers;

[Authorize]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class JobController(
    IMapper mapper,
    JobService jobService,
    IUserContextService userContextService) : UserContextController(userContextService)
{
    [HttpGet("all")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IEnumerable<JobDetailsDto>> GetAllJobs()
    {
        var entities = await jobService.GetJobsAsync();
        return mapper.Map<JobDetailsDto[]>(entities);
    }

    [HttpGet("visible")]
    public async Task<IEnumerable<JobDto>> GetVisibleJobs()
    {
        var entities = await jobService.GetVisibleJobsAsync();
        return mapper.Map<JobDto[]>(entities);
    }

    [HttpPatch("{id:int}/visible")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<JobDetailsDto> PatchJobVisibility(
        int id, [FromBody] bool isVisible)
    {
        var entity = await jobService.UpdateJobVisibilityAsync(id, isVisible);
        return mapper.Map<JobDetailsDto>(entity);
    }

    [HttpPatch("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<JobDetailsDto> PatchJobTitle(
        int id, [FromBody] string title)
    {
        var entity = await jobService.UpdateJobTitleAsync(id, title);
        return mapper.Map<JobDetailsDto>(entity);
    }

    [HttpPost]
    [Authorize(Policy = "AdminOnly")]
    public async Task<JobDetailsDto> AddJob([FromBody] string title)
    {
        var entity = await jobService.AddJobAsync(title);
        return mapper.Map<JobDetailsDto>(entity);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<JobDetailsDto> DeleteJob(int id)
    {
        var entity = await jobService.RemoveJobAsync(id);
        return mapper.Map<JobDetailsDto>(entity);
    }
}
