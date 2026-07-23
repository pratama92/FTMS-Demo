using System.ComponentModel.DataAnnotations;

namespace FTMS.Application.UseCases.PersonManagement.UpdatePerson
{
    public class UpdatePersonCommand
    {
        public Guid PersonId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
    }
}
