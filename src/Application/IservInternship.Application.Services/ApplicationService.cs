using IservInternship.Application.Infrastructure;
using IservInternship.Domain.Application.Entities;
using Microsoft.EntityFrameworkCore;

namespace IservInternship.Application.Services;

public class ApplicationService(ApplicationContext context)
{
    public Task<ApplicationEntity[]> GetApplicationsAsync()
    {
        return context.Applications.ToArrayAsync();
    }

    public Task<ApplicationEntity?> GetApplicationByIdAsync(Guid id)
    {
        return context.Applications.SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new ArgumentException($"Application is not exist with Id = {id}");
    }

    public Task<ApplicationEntity?> GetApplicationByUserUidAsync(Guid userUid)
    {
        return context.Applications.SingleOrDefaultAsync(x => x.UserUid == userUid)
            ?? throw new ArgumentException($"Application is not exist with userUid = {userUid}");
    }

    public async Task<ApplicationEntity> AddApplicationAsync(ApplicationEntity entity)
    {
        var entry = await context.Applications.AddAsync(entity);
        await context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<ApplicationEntity> UpdateApplicationAsync(Guid id, ApplicationEntity entity)
    {
        var existingEntity = await context.Applications.SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new ArgumentException($"Application is not exist with Id = {id}");

        existingEntity.FirstName = entity.FirstName;
        existingEntity.LastName = entity.LastName;
        existingEntity.Email = entity.Email;
        existingEntity.PhoneNumber = entity.PhoneNumber;
        existingEntity.AboutMe = entity.AboutMe;
        existingEntity.Status = entity.Status;

        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<ApplicationEntity> RemoveApplicationAsync(Guid id)
    {
        var existingEntity = await context.Applications.SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new ArgumentException($"Application is not exist with Id = {id}");

        await context.Applications
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        await context.SaveChangesAsync();
        return existingEntity;
    }
}
