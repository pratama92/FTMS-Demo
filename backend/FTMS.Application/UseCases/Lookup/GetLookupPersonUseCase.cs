using FTMS.Application.Common;
using FTMS.Application.Interfaces;

namespace FTMS.Application.UseCases.Lookup
{
    public class GetLookupPersonUseCase
    {
        private readonly ICurrentUser _currentUser;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly IPersonRepository _personRepository;

        public GetLookupPersonUseCase(ICurrentUser currentUser, IOrganizationValidator organizationValidator, IPersonRepository personRepository)
        {
            _currentUser = currentUser;
            _organizationValidator = organizationValidator;
            _personRepository = personRepository;
        }

        public async Task<BaseResponse<List<LookupResponse>>> ExecuteAsync()
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var person = await _personRepository.GetAllAsync(organizationId, false);
            var lookupResponse = person.Select(v => new LookupResponse
            {
                LookupId = v.PersonId,
                LookupName = v.Name,
            }).ToList();

            return new BaseResponse<List<LookupResponse>>
            {
                Success = true,
                Message = "Person lookup retrieved succesfully",
                Data = lookupResponse
            };
        }

    }
}
