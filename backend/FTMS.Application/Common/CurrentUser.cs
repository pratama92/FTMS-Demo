using System.Security.Claims;
using FTMS.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FTMS.Infrastructure.Security;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    private ClaimsPrincipal? User =>
        _httpContextAccessor.HttpContext?.User;

    public Guid OrganizationId
    {
        get
        {
            return Guid.Parse(
                User?.FindFirst("org_id")?.Value
                ?? throw new UnauthorizedAccessException("OrganizationId missing")
            );
        }
    }

    public Guid PersonId
    {
        get
        {
            return Guid.Parse(
                    User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new UnauthorizedAccessException("PersonId missing")
            );
        }
    }

    public string Role
    {
        get
        {
            return User?.FindFirst(ClaimTypes.Role)?.Value
            ?? throw new UnauthorizedAccessException("Role missing");
        }
    }
}