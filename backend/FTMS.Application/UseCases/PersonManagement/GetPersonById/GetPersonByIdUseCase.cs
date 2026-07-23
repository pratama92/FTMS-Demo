using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.PersonManagement.Dtos;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.PersonManagement.GetPersonById
{
    public class GetPersonByIdUseCase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public GetPersonByIdUseCase(IPersonRepository personRepository, IOrganizationValidator organizationValdiator, ICurrentUser currentUser)
        {
            _personRepository = personRepository;
            _organizationValidator = organizationValdiator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<PersonResponse>> ExecuteAsync(Guid personId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                throw new NotFoundException("Person not found.", ErrorCodes.PersonNotFound);
            }

            var personResponse = new PersonResponse
            {
                PersonId = person.PersonId,
                Name = person.Name,
                Email = person.Email,
                Phone = person.Phone,
                OrganizationId = person.OrganizationId,
                Roles = Enum.GetValues<PersonRoleEnum>()
                    .Where(role => person.Roles.HasFlag(role))
                    .Select(role => role.ToString())
                    .ToList()
            };

            return new BaseResponse<PersonResponse>
            {
                Success = true,
                Message = "Person retrieved successfully",
                Data = personResponse,
            };
        }
    }
}
