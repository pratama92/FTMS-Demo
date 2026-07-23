using FTMS.Application.Common;
using FTMS.Application.Interfaces;
using FTMS.Domain.Shared;

namespace FTMS.Application.UseCases.UserManagement.Login
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private const string InvalidCredentialsMessage = "Invalid username or password.";

        public LoginUseCase(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<BaseResponse<LoginResponse>> ExecuteAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user == null)
            {
                throw new BusinessException(InvalidCredentialsMessage, ErrorCodes.InvalidCredential);
            }

            var validPassword = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (!validPassword)
            {
                throw new BusinessException(InvalidCredentialsMessage, ErrorCodes.InvalidCredential);
            }

            var token = _jwtTokenGenerator.GenerateToken(user);

            var loginResponse = new LoginResponse
            {
                Token = token,
                Username = user.Username,
                Role = user.Role
            };

            return new BaseResponse<LoginResponse>
            {
                Success = true,
                Message = "Login Sucessfull",
                Data = loginResponse
            };
        }
    }
}