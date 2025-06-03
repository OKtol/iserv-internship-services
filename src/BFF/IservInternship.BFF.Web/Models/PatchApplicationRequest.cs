using IservInternship.Domain.Application.Enums;

namespace IservInternship.BFF.Web.Models;

public class PatchApplicationRequest
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }

    public string AboutMe { get; set; } = null!;

    public required Status Status { get; set; }
}
