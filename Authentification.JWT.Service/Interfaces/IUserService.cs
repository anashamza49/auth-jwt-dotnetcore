using Authentification.JWT.Service.DTOs;

namespace Authentification.JWT.Service.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<UserDto> RegisterUserAsync(string username, string email, string password);
        Task<bool> VerifyPassword(int userId, string password);
        Task<UserDto?> AuthenticateAsync(string username, string password);
    }
}
