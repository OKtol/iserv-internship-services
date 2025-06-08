namespace IservInternship.BFF.Web.Models;

public class CreateApplicationRequest
{
    public int JobId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PhoneNumber { get; set; }

    public string AboutMe { get; set; } = null!;
}
