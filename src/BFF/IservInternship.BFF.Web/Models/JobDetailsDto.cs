namespace IservInternship.BFF.Web.Models;

public class JobDetailsDto
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public bool IsVisible { get; set; }
}
