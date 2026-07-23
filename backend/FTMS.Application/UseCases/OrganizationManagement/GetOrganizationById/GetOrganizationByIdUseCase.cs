using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.OrganizationManagement.Dto;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.OrganizationManagement.GetOrganizationById
{
    public class GetOrganizationByIdUseCase
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationByIdUseCase(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<BaseResponse<OrganizationResponse>> ExecuteAsync(Guid organizationId)
        {
            var organization = await _organizationRepository.GetByIdAsync(organizationId);
            if (organization == null)
            {
                throw new NotFoundException("Organization not found.", ErrorCodes.OrganizationNotFound);
            }

            var orgResponse = new OrganizationResponse
            {
                OrganizationId = organization.OrganizationId,
                Name = organization.Name,
                Description = organization.Description,
                Status = organization.Status.ToString(),
            };

            return new BaseResponse<OrganizationResponse>
            {
                Success = true,
                Message = "Organization retrieved successfully",
                Data = orgResponse,
            };
        }
    }
}
