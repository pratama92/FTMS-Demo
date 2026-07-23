namespace FTMS.Application.UseCases.PersonManagement.CreatePerson
{
    public class CreatePersonCommand
    {
        public Guid? OrganizationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}