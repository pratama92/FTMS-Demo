using FTMS.Domain.Enums;

namespace FTMS.Application.UseCases.OrganizationManagement.Dto
{
    public class OrganizationResponse
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
