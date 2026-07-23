using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTMS.Application.Interfaces
{
    public interface IOrganizationRepository
    {
        Task AddAsync(Organization organization);
        Task<bool> ExistsAsync(Guid organizationId);
        Task<Organization?> GetByIdAsync(Guid organizationId);
        Task<bool> GetByNameAsync(string organizationName);
        Task<List<Organization>> GetAllAsync(OrganizationStatusEnum? status = null);
        Task UpdateAsync(Organization organization);
    }
}
