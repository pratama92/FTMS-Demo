using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.PersonManagement.UpdatePerson
{
    public class UpdatePersonUseCase
    {
        private readonly IPersonRepository personRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public UpdatePersonUseCase(IPersonRepository personRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            this.personRepository = personRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(UpdatePersonCommand request)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            var person = await personRepository.GetByIdAsync(request.PersonId);
            if (person == null)
            {
                throw new NotFoundException("Person not found.", ErrorCodes.PersonNotFound);
            }

            if (person.Name != request.Name || person.Phone != request.Phone || person.Email != request.Email)
            {
                person.UpdateContact(request.Name, request.Email, request.Phone);
            }

            // Persist changes after mutating the person entity
            await personRepository.UpdateAsync(person);

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Person updated successfully",
                Data = true,
            };
        }
    }
}
