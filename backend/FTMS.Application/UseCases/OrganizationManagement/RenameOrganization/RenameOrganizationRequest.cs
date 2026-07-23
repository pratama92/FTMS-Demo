namespace FTMS.Application.UseCases.OrganizationManagement.RenameOrganization
{
    public class RenameOrganizationRequest
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
