using Asp.Versioning;
using AutoMapper;
using IservInternship.Application.Services;
using IservInternship.BFF.Web.Models;
using IservInternship.Domain.Application.Entities;
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

    [HttpPatch("{id:int}")]
    public async Task<JobDetailsDto> PatchJobVisibility(
        int id, [FromBody] bool isVisible)
    {
        var entity = await jobService.UpdateJobVisibilityAsync(id, isVisible);
        return mapper.Map<JobDetailsDto>(entity);
    }
}
