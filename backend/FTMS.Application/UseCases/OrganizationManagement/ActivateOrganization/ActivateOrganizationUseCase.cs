using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.OrganizationManagement.ActivateOrganization
{
    public class ActivateOrganizationUseCase
    {
        private readonly IOrganizationRepository _organizationRepository;

        public ActivateOrganizationUseCase(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid organizationId)
        {
            var organization = await _organizationRepository.GetByIdAsync(organizationId);
            if (organization == null)
            {
                throw new NotFoundException("Organization not found.", ErrorCodes.OrganizationNotFound);
            }

            organization.Activate();

            await _organizationRepository.UpdateAsync(organization); // Ensure soft-delete is persisted
            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Organization activated successfully",
                Data = true,
            };
        }
    }
}
