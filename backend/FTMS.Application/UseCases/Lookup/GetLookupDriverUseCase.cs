using FTMS.Application.Common;
using FTMS.Application.Interfaces;

namespace FTMS.Application.UseCases.Lookup
{
    public class GetLookupDriverUseCase
    {
        private readonly ICurrentUser _currentUser;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly IPersonRepository _personRepository;

        public GetLookupDriverUseCase(ICurrentUser currentUser, IOrganizationValidator organizationValidator, IPersonRepository personRepository)
        {
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
            _personRepository = personRepository;
        }

        public async Task<BaseResponse<List<LookupResponse>>> ExecuteAsync()
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var vehicle = await _personRepository.GetAllDriverAsync(organizationId, false);
            var lookupResponse = vehicle.Select(v => new LookupResponse
            {
                LookupId = v.PersonId,
                LookupName = v.Name,
            }).ToList();

            return new BaseResponse<List<LookupResponse>>
            {
                Success = true,
                Message = "Driver lookup retrieved successfully",
                Data = lookupResponse
            };
        }
    }
}
