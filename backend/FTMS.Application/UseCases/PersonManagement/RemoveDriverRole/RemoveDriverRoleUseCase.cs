using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.PersonManagement.RemoveDriverRole
{
    public class RemoveDriverRoleUseCase
    {
        private readonly IPersonRepository _personRepository;

        public RemoveDriverRoleUseCase(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<BaseResponse<bool>> ExecuteAsync(Guid personId)
        {
            // 1. check if person exists
            var person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                throw new NotFoundException("Person not found.", ErrorCodes.PersonNotFound);
            }

            // 2. remove driver role
            person.RemoveDriverRole();
            await _personRepository.UpdateAsync(person);


            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Driver role removed successfully",
                Data = true,
            };
        }
    }
}
