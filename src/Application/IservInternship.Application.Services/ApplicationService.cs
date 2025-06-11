using IservInternship.Application.Infrastructure;
using IservInternship.Commons.Exceptions;
using IservInternship.Domain.Application.Entities;
using IservInternship.Domain.Application.Enums;
using Microsoft.EntityFrameworkCore;

namespace IservInternship.Application.Services;

public class ApplicationService(ApplicationContext context)
{
    public Task<ApplicationEntity[]> GetApplicationsAsync()
    {
        return context.Applications
            .Include(x => x.Job)
            .ToArrayAsync();
    }

    public async Task<ApplicationEntity> GetApplicationByIdAsync(Guid id)
    {
        return await context.Applications
            .Include(x => x.Job)
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Application is not exist with Id = {id}");
    }

    public async Task<ApplicationEntity> UpdateApplicationStatusAsync(Guid id, Status status)
    {
        var existingEntity = await context.Applications
            .Include(x => x.Job)
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Application is not exist with Id = {id}");

        existingEntity.Status = status;

        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<ApplicationEntity> UpdateApplicationSolutionStatusAsync(Guid id, Status status)
    {
        var existingEntity = await context.Applications
            .Include(x => x.Job)
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Application is not exist with Id = {id}");

        existingEntity.SolutionStatus = status;

        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<ApplicationEntity> GetApplicationByUserUidAsync(Guid userUid)
    {
        return await context.Applications
            .Include(x => x.Job)
            .SingleOrDefaultAsync(x => x.UserUid == userUid)
            ?? throw new NotFoundException($"Application is not exist with userUid = {userUid}");
    }

    public async Task<ApplicationEntity> AddApplicationAsync(
        Guid userUid,
        string email,
        int jobId,
        string firstName,
        string lastName,
        string phoneNumber,
        string aboutMe
        )
    {
        var entity = new ApplicationEntity
        {
            UserUid = userUid,
            Email = email,
            JobId = jobId,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            AboutMe = aboutMe
        };
        var entry = await context.Applications.AddAsync(entity);
        await context.SaveChangesAsync();

        entry.Entity.Job = await context.Jobs
            .SingleOrDefaultAsync(x => x.Id == entity.JobId)
            ?? throw new NotFoundException($"Job is not exist with Id = {entity.JobId}");
        return entry.Entity;
    }

    public async Task<ApplicationEntity> UpdateApplicationAsync(
        Guid id, 
        int jobId,
        string firstName,
        string lastName,
        string phoneNumber,
        string aboutMe
        )
    {
        var existingEntity = await context.Applications
            .Include(x => x.Job)
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Application is not exist with Id = {id}");

        existingEntity.JobId = jobId;
        existingEntity.FirstName = firstName;
        existingEntity.LastName = lastName;
        existingEntity.PhoneNumber = phoneNumber;
        existingEntity.AboutMe = aboutMe;

        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<ApplicationEntity> UpdateApplicationSolutionAsync(Guid id, string solution)
    {
        var existingEntity = await context.Applications
            .Include(x => x.Job)
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Application is not exist with Id = {id}");

        existingEntity.Solution = solution;

        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<ApplicationEntity> UpdateApplicationAnswerAsync(Guid id, string answer)
    {
        var existingEntity = await context.Applications
            .Include(x => x.Job)
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Application is not exist with Id = {id}");

        existingEntity.Answer = answer;

        await context.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<ApplicationEntity> RemoveApplicationAsync(Guid id)
    {
        var existingEntity = await context.Applications
            .Include(x => x.Job)
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException($"Application is not exist with Id = {id}");

        await context.Applications
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        await context.SaveChangesAsync();
        return existingEntity;
    }
}
