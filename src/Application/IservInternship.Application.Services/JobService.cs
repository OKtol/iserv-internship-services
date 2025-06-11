using IservInternship.Application.Infrastructure;
using IservInternship.Commons.Exceptions;
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
            ?? throw new NotFoundException($"Job is not exist with Id = {id}");

        existingEntity.IsVisible = isVisible;

        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<JobEntity> UpdateJobTitleAsync(int id, string title)
    {
        var existingEntity = await context.Jobs
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Job is not exist with Id = {id}");

        existingEntity.Title = title;

        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<JobEntity> AddJobAsync(string title)
    {
        var entity = new JobEntity
        {
            Title = title,
            IsVisible = false
        };
        var entry = await context.Jobs.AddAsync(entity);
        await context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<JobEntity> RemoveJobAsync(int id)
    {
        var existingEntity = await context.Jobs
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Job is not exist with Id = {id}");

        await context.Jobs
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        await context.SaveChangesAsync();
        return existingEntity;
    }
}
