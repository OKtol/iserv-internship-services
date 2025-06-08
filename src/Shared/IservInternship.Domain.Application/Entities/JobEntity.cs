namespace IservInternship.Domain.Application.Entities;

public class JobEntity
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public bool IsVisible { get; set; }

    public IEnumerable<ApplicationEntity> Applications { get; set; } = null!;
}
