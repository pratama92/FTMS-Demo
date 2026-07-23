using FTMS.Domain.Entities;

namespace FTMS.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
