using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Application.UseCases.PersonManagement.Dtos;
using FTMS.Domain.Enums;

namespace FTMS.Application.UseCases.PersonManagement.GetPersons
{
    public class GetPersonsUseCase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public GetPersonsUseCase(IPersonRepository personRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            _personRepository = personRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<List<PersonResponse>>> ExecuteAsync(bool? isDeleted = null)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            Guid? filterGuid = null;
            if (_currentUser.Role != UserRoleEnum.Admin.ToString())
            {
                filterGuid = organizationId;
            }

            var persons = await _personRepository.GetAllAsync(filterGuid, isDeleted);
            var personResponses = persons.Select(person => new PersonResponse
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
            }).ToList();



            return new BaseResponse<List<PersonResponse>>
            {
                Success = true,
                Message = "Persons retrieved successfully",
                Data = personResponses
            };
        }
    }
}
