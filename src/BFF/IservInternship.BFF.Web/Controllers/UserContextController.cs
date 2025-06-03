using IservInternship.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace IservInternship.BFF.Web.Controllers;

[ApiController]
public abstract class UserContextController(IUserContextService userContextService) : ControllerBase
{
    protected string? GetRetrieveUserIdentificator()
            => userContextService.RetrieveUserIdentificator();
}
