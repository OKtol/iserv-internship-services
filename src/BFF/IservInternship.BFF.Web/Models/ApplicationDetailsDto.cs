using IservInternship.Application.Services;
using IservInternship.Domain.Application.Enums;

namespace IservInternship.BFF.Web.Models;

public class ApplicationDetailsDto
{
    public required Guid Id { get; set; }

    public required int JobId { get; set; }

    public required string JobTitle { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }

    public string AboutMe { get; set; } = string.Empty;

    public required Status Status { get; set; }

    public string Solution { get; set; } = string.Empty;

    public Status VerificationStatus { get; set; }

    public string Answer { get; set; } = string.Empty;

    public Status SolutionStatus { get; set; }

    public TestTaskDto TestTask { get; set; } = null!;
}
