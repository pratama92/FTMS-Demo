using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.Common
{
    public class OrganizationValidator : IOrganizationValidator
    {
        private readonly IOrganizationRepository _organizationRepository;
        public OrganizationValidator(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task ValidateAsync(Guid organizationId)
        {
            if (!await _organizationRepository.ExistsAsync(organizationId))
                throw new BusinessException("Organization is not exist.", ErrorCodes.OrganizationRequired);
        }
    }
}

