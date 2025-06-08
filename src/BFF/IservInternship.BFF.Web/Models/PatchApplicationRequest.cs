namespace IservInternship.BFF.Web.Models;

public class PatchApplicationRequest
{
    public int JobId { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string PhoneNumber { get; set; }

    public string AboutMe { get; set; } = null!;
}
