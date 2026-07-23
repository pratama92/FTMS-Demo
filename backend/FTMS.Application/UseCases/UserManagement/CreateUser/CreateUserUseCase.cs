using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Entities;
using FTMS.Domain.Enums;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.UserManagement.CreateUser
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPersonRepository _personRepository;
        private readonly IOrganizationRepository _organizationRepository;

        public CreateUserUseCase(IUserRepository userRepository, IPasswordHasher passwordHasher, IPersonRepository personRepository, IOrganizationRepository organizationRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _personRepository = personRepository;
            _organizationRepository = organizationRepository;
        }

        public async Task<BaseResponse<string>> ExecuteAsync(CreateUserRequest request)
        {
            // check enum
            if (!Enum.TryParse<UserRoleEnum>(request.Role, true, out var role))
            {
                throw new BusinessException("Invalid role.", ErrorCodes.InvalidData);
            }

            // Check if the user already exists
            if (await _userRepository.ExistsByUsernameAsync(request.Username))
            {
                throw new BusinessException("User with the same username already exists.", ErrorCodes.UserNameExists);
            }

            if (request.PersonId != Guid.Empty && !await _personRepository.ExistsByIdAsync(request.PersonId))
            {
                throw new NotFoundException("Person is not found.", ErrorCodes.PersonNotFound);
            }

            if (request.OrganizationId != Guid.Empty && !await _organizationRepository.ExistsAsync(request.OrganizationId))
            {
                throw new NotFoundException("Organization is not found.", ErrorCodes.OrganizationNotFound);
            }

            // hash password
            var hashedPassword = _passwordHasher.Hash(request.Password);

            var user = User.Create(request.OrganizationId, request.PersonId, request.Username, hashedPassword, role);

            // Save the new user to the repository
            await _userRepository.AddAsync(user);

            // response
            return new BaseResponse<string>
            {
                Success = true,
                Message = "User created successfully.",
                Data = request.Username
            };

        }
    }
}
