using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.PersonManagement.AddDriverRole
{
    public class AddDriverRoleUseCase
    {
        private readonly IPersonRepository _personRepository;

        public AddDriverRoleUseCase(IPersonRepository personRepository)
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

            // 2. add driver role
            person.AddDriverRole();

            await _personRepository.UpdateAsync(person);

            return new BaseResponse<bool>
            {
                Success = true,
                Message = "Driver role added successfully",
                Data = true,
            };
        }
    }
}
