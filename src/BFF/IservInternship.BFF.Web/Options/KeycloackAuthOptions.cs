using System.ComponentModel.DataAnnotations;

namespace IservInternship.BFF.Web.Options;

public class KeycloackAuthOptions
{
    [Required]
    public string AuthorityUri { get; set; } = default!;

    [Required]
    public string Realm { get; set; } = default!;

    [Required]
    public string Audience { get; set; } = default!;
}
