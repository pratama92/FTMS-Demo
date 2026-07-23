using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.OrganizationManagement.Dto;
using FTMS.Domain.Enums;

namespace FTMS.Application.UseCases.OrganizationManagement.GetOrganizations
{
    public class GetOrganizationsUseCase
    {
        private readonly IOrganizationRepository _organizationRepository;

        public GetOrganizationsUseCase(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<BaseResponse<List<OrganizationResponse>>> ExecuteAsync(OrganizationStatusEnum? status = null)
        {
            var organizations = await _organizationRepository.GetAllAsync(status);
            var orgResponses = organizations.Select(organization => new OrganizationResponse
            {
                OrganizationId = organization.OrganizationId,
                Name = organization.Name,
                Description = organization.Description,
                Status = organization.Status.ToString(),
            }).ToList();

            return new BaseResponse<List<OrganizationResponse>>
            {
                Success = true,
                Message = "Organizations retrieved successfully",
                Data = orgResponses,
            };
        }
    }
}
