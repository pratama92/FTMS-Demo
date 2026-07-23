using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.PersonManagement.CreatePerson
{
    public class CreatePersonUseCase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IOrganizationValidator _organizationValidator;
        private readonly ICurrentUser _currentUser;

        public CreatePersonUseCase(IPersonRepository personRepository, IOrganizationValidator organizationValidator, ICurrentUser currentUser)
        {
            _personRepository = personRepository;
            _organizationValidator = organizationValidator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<Guid>> ExecuteAsync(CreatePersonCommand request)
        {
            // check organizationId (infrastructure via interface)
            var organizationId = _currentUser.OrganizationId;
            await _organizationValidator.ValidateAsync(organizationId);

            // only for Admin role, allow to create person for other organization
            if (_currentUser.Role == UserRoleEnum.Admin.ToString())
            {
                if (request.OrganizationId.HasValue)
                {
                    organizationId = request.OrganizationId.Value;
                    await _organizationValidator.ValidateAsync(organizationId);
                }
                else
                {
                    throw new BusinessException("OrganizationId is required for Admin role.", ErrorCodes.OrganizationRequiredAdmin);
                }
            }

            // 1. check duplicate email (infrastructure via interface)
            if (await _personRepository.ExistsByEmailAsync(request.Email))
            {
                throw new BusinessException("Email already exists.", ErrorCodes.PersonEmailExists);
            }

            // 2. create domain entity
            var person = Person.Create(
                request.Name,
                request.Email,
                request.Phone,
                organizationId
            );

            // 4. save to DB
            await _personRepository.AddAsync(person);

            // 5. return response
            return new BaseResponse<Guid>
            {
                Success = true,
                Message = "Person created successfully",
                Data = person.PersonId
            };
        }
    }
}