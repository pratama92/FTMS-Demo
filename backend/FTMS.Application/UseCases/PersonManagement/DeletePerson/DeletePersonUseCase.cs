using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.PersonManagement.DeletePerson
{
    public class DeletePersonUseCase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IOrganizationValidator _organizationValdiator;
        private readonly ICurrentUser _currentUser;

        public DeletePersonUseCase(IPersonRepository personRepository, IOrganizationValidator organizationValdiator, ICurrentUser currentUser)
        {
            _personRepository = personRepository;
            _organizationValdiator = organizationValdiator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid personId)
        {
            var organizationId = _currentUser.OrganizationId;
            await _organizationValdiator.ValidateAsync(organizationId);

            var person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                throw new NotFoundException("Person not found.", ErrorCodes.PersonNotFound);
            }
            person.Delete();
            await _personRepository.UpdateAsync(person); // Ensure soft-delete is persisted
            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Person deleted successfully",
                Data = true,
            };
        }
    }
}
