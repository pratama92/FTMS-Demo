namespace FTMS.Application.Common
{
    public interface IOrganizationValidator
    {
        Task ValidateAsync(Guid organizationId);

    }
}
