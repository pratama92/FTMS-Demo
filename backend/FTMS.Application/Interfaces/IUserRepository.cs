using FTMS.Domain.Entities;

namespace FTMS.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetByIdAsync(Guid userId);

        Task AddAsync(User user);

        Task<bool> ExistsByUsernameAsync(string username);
    }
}
