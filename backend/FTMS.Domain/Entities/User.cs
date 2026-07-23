using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; private set; }
        public Guid OrganizationId { get; private set; }
        public Guid PersonId { get; private set; }
        public Person Person { get; private set; } = null!;
        public string Username { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRoleEnum Role { get; private set; }
        public Organization Organization { get; private set; } = null!;
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }

        private User() { }


        private User(
            Guid organizationId,
            Guid personId,
            string username,
            string passwordHash,
            UserRoleEnum role)
        {
            if (organizationId == Guid.Empty)
                throw new BusinessException("Organization is required.", "ORGANIZATION_REQUIRED");

            if (personId == Guid.Empty)
                throw new BusinessException("Person is required.", "PERSON_REQUIRED");

            if (string.IsNullOrWhiteSpace(username))
                throw new BusinessException("Username cannot be empty.", "USER_NAME_REQUIRED");

            OrganizationId = organizationId;
            PersonId = personId;
            Username = username.Trim().ToLower();
            PasswordHash = passwordHash;
            Role = role;

            UserId = Guid.NewGuid();

            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }


        public static User Create(
            Guid organizationId,
            Guid personId,
            string username,
            string passwordHash,
            UserRoleEnum role)
        {
            return new User(
                organizationId,
                personId,
                username,
                passwordHash,
                role);
        }


        public void ChangePassword(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new BusinessException("Password is required.", "PASSWORD_REQUIRED");

            PasswordHash = passwordHash;
            UpdatedAt = DateTimeOffset.UtcNow;
        }


        public void Delete()
        {
            if (IsDeleted)
                throw new BusinessException("User already deleted.", "USER_IS_DELETED");

            IsDeleted = true;
            DeletedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }
    }
}