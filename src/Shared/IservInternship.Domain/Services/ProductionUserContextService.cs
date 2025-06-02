using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace IservInternship.Domain.Services;

public class ProductionUserContextService(IHttpContextAccessor currentHttpContext) : IUserContextService
{
    private readonly ClaimsPrincipal _user = currentHttpContext.HttpContext?.User
        ?? throw new ArgumentNullException(nameof(currentHttpContext));

    string? IUserContextService.RetrieveUserIdentificator()
        => _user.FindFirstValue(ClaimTypes.NameIdentifier);
}
