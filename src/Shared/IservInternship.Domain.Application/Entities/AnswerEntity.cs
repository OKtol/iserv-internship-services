using IservInternship.Domain.Application.Enums;

namespace IservInternship.Domain.Application.Entities;

public class AnswerEntity
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;

    public required Status Status { get; set; }

    public Guid ApplicationId { get; set; }

    public ApplicationEntity Application { get; set; } = null!;
}
