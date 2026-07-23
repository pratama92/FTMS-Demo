namespace FTMS.API.Dto.Person.Request
{
    public class CreatePersonRequest
    {
        public Guid? OrganizationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
