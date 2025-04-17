using Authentification.JWT.DAL.Models;
using Authentification.JWT.DAL.Repositories;
using Authentification.JWT.Service.DTOs;
using Authentification.JWT.Service.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Authentification.JWT.Service.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper, ILogger<UserService> logger) : IUserService
    {
        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            logger.LogDebug("Looking for the {Username}", username);

            var user = await userRepository.GetUserByUsernameAsync(username);

            if (user == null)
            {
                logger.LogWarning("User {Username} couldn't be found", username);
                return null;
            }

            logger.LogInformation("User {UserId} found", user.Id);
            return mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> RegisterUserAsync(string username, string email, string password)
        {
            logger.LogInformation("attemp to register for {Username}", username);

            if (await userRepository.GetUserByUsernameAsync(username) != null)
            {
                logger.LogWarning("Username {Username} already exists", username);
                throw new Exception("Username couldn't be found");
            }

            var newUser = new User
            {
                Username = username,
                Email = email,
                PasswordHash = PasswordHasher.HashPassword(password)
            };

            var createdUser = await userRepository.RegisterUserAsync(newUser);
            logger.LogInformation("new user registered : {UserId}", createdUser.Id);

            return mapper.Map<UserDto>(createdUser);
        }

        public async Task<bool> VerifyPassword(int userId, string password)
        {
            logger.LogDebug("verifying password for {UserId}", userId);

            var user = await userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                logger.LogWarning("the user : {UserId} couldn't be found", userId);
                return false;
            }

            var isValid = PasswordHasher.VerifyPassword(password, user.PasswordHash);
            logger.LogInformation("verifying password {UserId} : {Result}", userId, isValid ? "Valid" : "Invalid");

            return isValid;
        }
        public async Task<UserDto?> AuthenticateAsync(string username, string password)
        {
            var user = await userRepository.GetUserByUsernameAsync(username);
            if (user == null) return null;

            var isValid = PasswordHasher.VerifyPassword(password, user.PasswordHash);
            return isValid ? mapper.Map<UserDto>(user) : null;
        }

    }
}