using IservInternship.Domain.Application.Enums;

namespace IservInternship.Domain.Application.Entities;

public class ApplicationEntity
{
    public Guid Id { get; set; }

    public Guid UserUid { get; set; }

    public int JobId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }

    public string AboutMe { get; set; } = string.Empty;

    public Status Status { get; set; }

    public string Solution { get; set; } = string.Empty;
    
    public Status VerificationStatus { get; set; }

    public string Answer { get; set; } = string.Empty;

    public Status SolutionStatus { get; set; }

    public string TestTask { get; set; } = string.Empty;

    public string CorrectAnswer { get; set; } = string.Empty;

    public JobEntity Job { get; set; } = null!;
}
