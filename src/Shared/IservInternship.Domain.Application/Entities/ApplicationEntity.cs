using IservInternship.Domain.Application.Enums;

namespace IservInternship.Domain.Application.Entities;

public class ApplicationEntity
{
    public Guid Id { get; set; }

    public Guid UserUid { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }

    public string AboutMe { get; set; } = null!;

    public required Status Status { get; set; }

    public AnswerEntity Answer { get; set; } = null!;
}
