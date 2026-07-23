namespace FTMS.Application.Interfaces
{
    public interface ICurrentUser
    {
        Guid OrganizationId { get; }
        Guid PersonId { get; }
        string Role { get; }
    }
}
