using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FTMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _context.User.AnyAsync(u => u.Username == username);
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.User.Include(u => u.Person).FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.User.Include(u => u.Person).FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
