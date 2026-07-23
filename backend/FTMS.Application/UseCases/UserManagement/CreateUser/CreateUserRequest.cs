using System.ComponentModel;

namespace FTMS.Application.UseCases.UserManagement.CreateUser
{
    public class CreateUserRequest
    {
        public Guid OrganizationId { get; set; }

        public Guid PersonId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        [DefaultValue("Dispatcher")]
        public string Role { get; set; } = "Dispatcher";
    }
}
