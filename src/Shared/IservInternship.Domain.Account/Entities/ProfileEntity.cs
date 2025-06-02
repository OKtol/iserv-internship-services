using IservInternship.Domain.Account.Enums;

namespace IservInternship.Domain.Account.Entities;

public class ProfileEntity
{
    public Guid Id { get; set; }

    public Guid UserUid { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber {  get; set; }

    public required string AboutMe { get; set; }

    public Status ApplicationStatus { get; set; }

    public Guid AnswerId { get; set; }
}
