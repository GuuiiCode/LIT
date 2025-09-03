using LIT.Application.ViewModels;

namespace LIT.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResultViewModel> RegisterAsync(UserViewModel userViewModel);
        Task<LoginResponseViewModel> LoginAsync(LoginViewModel loginViewModel);
    }
}
