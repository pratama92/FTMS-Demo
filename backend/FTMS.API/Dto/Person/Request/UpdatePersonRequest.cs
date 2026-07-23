using System.ComponentModel.DataAnnotations;

namespace FTMS.API.Dto.Person.Request
{
    public class UpdatePersonRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        public Guid OrganizationId { get; set; }
    }
}
