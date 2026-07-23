using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.OrganizationManagement.RenameOrganization
{
    public class RenameOrganizationUseCase
    {
        private readonly IOrganizationRepository _organizationRepository;
        public RenameOrganizationUseCase(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(RenameOrganizationRequest request)
        {
            var organization = await _organizationRepository.GetByIdAsync(request.OrganizationId);
            if (organization == null)
            {
                throw new NotFoundException("Organization not found.", ErrorCodes.OrganizationNotFound);
            }

            organization.Rename(request.Name);

            await _organizationRepository.UpdateAsync(organization);
            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Organization renamed successfully",
                Data = true,
            };
        }
    }
}
