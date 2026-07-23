using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FTMS.Infrastructure.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Organization>> GetAllAsync(OrganizationStatusEnum? status = null)
        {
            IQueryable<Organization> query = _context.Organizations.AsNoTracking();

            if (status.HasValue)
            {
                var nonNullableStatus = status.Value;
                query = query.Where(p => p.Status == nonNullableStatus);
            }

            return await query.ToListAsync();
        }

        public async Task<Organization?> GetByIdAsync(Guid organizationId)
        {
            return await _context.Organizations.Where(o => o.OrganizationId == organizationId).SingleOrDefaultAsync();
        }

        public async Task<bool> GetByNameAsync(string organizationName)
        {
            return await _context.Organizations.Where(o => o.Name == organizationName).AnyAsync();
        }

        public async Task UpdateAsync(Organization organization)
        {
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Guid organizationId)
        {
            return await _context.Organizations.AnyAsync(o => o.OrganizationId == organizationId);
        }
    }
}
