using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FTMS.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var normalizedEmail = email.Trim().ToLowerInvariant();
            return await _context.Persons.AnyAsync(p => p.Email == normalizedEmail);
        }

        public async Task<bool> ExistsByIdAsync(Guid personId)
        {
            if (personId == Guid.Empty)
                return false;

            return await _context.Persons.AnyAsync(p => p.PersonId == personId);
        }

        public async Task<List<Person>> GetAllAsync(Guid? organizationId = null, bool? isDeleted = null)
        {
            IQueryable<Person> query = _context.Persons.AsNoTracking();

            if (organizationId.HasValue && organizationId != Guid.Empty)
            {
                query = query.Where(p => p.OrganizationId == organizationId.Value);
            }

            if (isDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == isDeleted.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Person>> GetAllDriverAsync(Guid? organizationId = null, bool? isDeleted = null)
        {
            IQueryable<Person> query = _context.Persons.AsNoTracking();

            if (organizationId.HasValue && organizationId != Guid.Empty)
            {
                query = query.Where(p => p.OrganizationId == organizationId.Value);
            }

            if (isDeleted.HasValue)
            {
                query = query.Where(p => p.IsDeleted == isDeleted.Value);
            }

            query = query.Where(p => (p.Roles & PersonRoleEnum.Driver) == PersonRoleEnum.Driver);

            return await query.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(Guid id)
        {
            return await _context.Persons.Where(x => x.PersonId == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }
    }
}