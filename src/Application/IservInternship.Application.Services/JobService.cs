using IservInternship.Application.Infrastructure;
using IservInternship.Domain.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace IservInternship.Application.Services;

public class JobService(ApplicationContext context)
{
    public Task<JobEntity[]> GetJobsAsync()
    {
        return context.Jobs.ToArrayAsync();
    }

    public Task<JobEntity[]> GetVisibleJobsAsync()
    {
        return context.Jobs.Where(x => x.IsVisible).ToArrayAsync();
    }

    public async Task<JobEntity> UpdateJobVisibilityAsync(int id, bool isVisible)
    {
        var existingEntity = await context.Jobs
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new ArgumentException($"Job is not exist with Id = {id}");

        existingEntity.IsVisible = isVisible;

        await context.SaveChangesAsync();
        return existingEntity;
    }
}
