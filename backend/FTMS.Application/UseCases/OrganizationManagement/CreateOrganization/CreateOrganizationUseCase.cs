using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.OrganizationManagement.CreateOrganization
{
    public class CreateOrganizationUseCase
    {
        private readonly IOrganizationRepository _organizationRepository;
        public CreateOrganizationUseCase(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<BaseResponse<Guid>> ExecuteAsync(CreateOrganizationRequest request)
        {
            // 1. check duplicate name (infrastructure via interface)
            if (await _organizationRepository.GetByNameAsync(request.Name))
            {
                throw new BusinessException("Organization name already exists.", ErrorCodes.OrganizationExists);
            }

            // 3. create domain entity
            var org = Organization.Create(
                request.Name,
                request.Description
            );

            // 4. save to DB
            await _organizationRepository.AddAsync(org);

            // 5. return response
            return new BaseResponse<Guid>
            {
                Success = true,
                Message = "Organization created successfully",
                Data = org.OrganizationId
            };
        }
    }
}
