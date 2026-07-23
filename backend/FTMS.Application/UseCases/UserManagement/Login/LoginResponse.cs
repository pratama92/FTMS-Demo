using FTMS.Domain.Enums;

namespace FTMS.Application.UseCases.UserManagement.Login
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public UserRoleEnum Role { get; set; }
    }
}
