using FTMS.Domain.Entities;

namespace FTMS.Application.Interfaces
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);

        Task<bool> ExistsByEmailAsync(string email);
        Task<bool> ExistsByIdAsync(Guid personId);

        Task<List<Person>> GetAllAsync(Guid? organizationId = null, bool? isDeleted = null);
        Task<List<Person>> GetAllDriverAsync(Guid? organizationId = null, bool? isDeleted = null);

        Task<Person?> GetByIdAsync(Guid id);

        Task UpdateAsync(Person person);
    }
}