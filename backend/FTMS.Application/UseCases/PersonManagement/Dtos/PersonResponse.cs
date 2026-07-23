namespace FTMS.Application.UseCases.PersonManagement.Dtos
{
    public class PersonResponse
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new();
        public Guid? OrganizationId { get; set; }
    }
}
