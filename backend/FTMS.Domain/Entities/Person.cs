using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities
{
    public class Person
    {
        public Guid PersonId { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public Guid OrganizationId { get; private set; }
        public Organization Organization { get; private set; } = null!;
        public PersonRoleEnum Roles { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTimeOffset? DeletedAt { get; private set; }

        private Person() { }

        private Person(string name, string email, string phone, Guid organizationId)
        {
            Validate(name, email);

            PersonId = Guid.NewGuid();
            Name = name;
            Email = email.Trim().ToLower();
            Phone = phone;
            OrganizationId = organizationId;
            Roles = PersonRoleEnum.Passenger;
            CreatedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        public static Person Create(string name, string email, string phone, Guid organizationId)
        {
            return new Person(name, email, phone, organizationId);
        }

        public void UpdateContact(string name, string email, string phone)
        {
            Validate(name, email);

            Name = name;
            Email = email.Trim().ToLower();
            Phone = phone;
            UpdatedAt = DateTimeOffset.Now;
        }

        public void AddDriverRole()
        {
            Roles |= PersonRoleEnum.Driver;
            UpdatedAt = DateTimeOffset.Now;
        }

        public void RemoveDriverRole()
        {
            Roles &= ~PersonRoleEnum.Driver;
            UpdatedAt = DateTimeOffset.Now;
        }

        public void EnsureCanDrive()
        {
            if (!Roles.HasFlag(PersonRoleEnum.Driver))
            {
                throw new BusinessException("Person does not have driver role.", ErrorCodes.PersonNotDriver);
            }
        }

        public void Delete()
        {
            if (IsDeleted)
                throw new BusinessException("Person already deleted.", ErrorCodes.PersonDeleted);

            IsDeleted = true;
            DeletedAt = DateTimeOffset.UtcNow;
            UpdatedAt = DateTimeOffset.UtcNow;
        }

        private static void Validate(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BusinessException("Name cannot be empty", ErrorCodes.PersonNameRequired);

            if (string.IsNullOrWhiteSpace(email))
                throw new BusinessException("Email cannot be empty", ErrorCodes.PersonEmailRequired);

            if (!email.Contains("@"))
                throw new BusinessException("Email is not valid", ErrorCodes.PersonEmailInvalid);
        }
    }
}