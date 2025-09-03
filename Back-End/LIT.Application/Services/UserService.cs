using AutoMapper;
using LIT.Application.Services.Interfaces;
using LIT.Application.ViewModels;
using LIT.Domain.Entities;
using LIT.Domain.Interfaces.Repositories;

namespace LIT.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ResultViewModel> RegisterAsync(UserViewModel userViewModel)
        {
            var existingUser = await _userRepository.GetByUserNameAsync(userViewModel.Name!);

            if (existingUser != null)
                return CreateResult(false, "User already exists");

            var user = _mapper.Map<User>(userViewModel);
            await _userRepository.InsertAsync(user);

            return CreateResult(true);
        }

        public async Task<LoginResponseViewModel> LoginAsync(LoginViewModel loginViewModel)
        {
            var user = await _userRepository.GetByUserNameAsync(loginViewModel.Name!);

            if (user == null || !user.VerifyPassword(loginViewModel.Password!))
                return CreateLoginResponse(false, "Invalid username or password");

            var loginResult = _mapper.Map<LoginResultViewModel>(user);

            return CreateLoginResponse(true, null, loginResult);
        }

        private ResultViewModel CreateResult(bool success, string? error = null)
        {
            return new ResultViewModel
            {
                Success = success,
                Error = error
            };
        }

        private LoginResponseViewModel CreateLoginResponse(bool success, string? error = null, LoginResultViewModel? loginData = null)
        {
            return new LoginResponseViewModel
            {
                ResultViewModel = CreateResult(success, error),
                LoginResultViewModel = loginData
            };
        }
    }
}
