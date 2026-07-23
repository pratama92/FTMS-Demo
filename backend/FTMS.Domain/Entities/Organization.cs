using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Domain.Entities;

public sealed class Organization
{
    public Guid OrganizationId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; } = string.Empty;
    public OrganizationStatusEnum Status { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    // Navigation Properties
    public IReadOnlyCollection<Person> Persons => _persons.AsReadOnly();
    public IReadOnlyCollection<Vehicle> Vehicles => _vehicles.AsReadOnly();
    public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private readonly List<Person> _persons = new List<Person>();
    private readonly List<Vehicle> _vehicles = new List<Vehicle>();
    private readonly List<Booking> _bookings = new List<Booking>();
    private readonly List<User> _users = new List<User>();

    private Organization() { }

    private Organization(
        string name,
        string? description)
    {
        Validate(name);

        OrganizationId = Guid.NewGuid();
        Name = name.Trim();
        Description = description?.Trim();

        Status = OrganizationStatusEnum.Active;
        CreatedAt = DateTimeOffset.UtcNow;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public static Organization Create(
        string name,
        string? description)
    {
        return new Organization(
            name,
            description);
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException("Organization name cannot be empty.", "ORGANIZATION_NAME_REQUIRED");

        Name = name.Trim();
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void ChangeDescription(string? description)
    {
        Description = description?.Trim();
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Activate()
    {
        if (Status == OrganizationStatusEnum.Active)
            return;

        Status = OrganizationStatusEnum.Active;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void Deactivate()
    {
        if (Status == OrganizationStatusEnum.Inactive)
            return;

        Status = OrganizationStatusEnum.Inactive;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    private static void Validate(
        string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException("Organization name cannot be empty.", "ORGANIZATION_NAME_REQUIRED");
    }
}