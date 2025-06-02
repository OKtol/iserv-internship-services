using IservInternship.Domain.Account.Enums;

namespace IservInternship.Domain.Account.Entities;

public class AnswerEntity
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;

    public Status AnswerStatus { get; set; }
}
