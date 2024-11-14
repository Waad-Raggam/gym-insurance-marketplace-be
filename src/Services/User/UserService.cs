using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using src.DTO;
using src.Entity;
using src.Repository;
using src.Utils;
using static src.DTO.UserDTO;

namespace src.Services.User
{
    /// <summary>
    /// Services Contain the business logic of your application and interact with entities, repositories, and other services.
    //  Services use DTOs to transfer data between different layers of the application, such as between the controller and the repository.
    /// </summary>
    public class UserService : IUserService
    {
        protected readonly UserRepository _userRepo;
        protected readonly IMapper _mapper;

        protected readonly IConfiguration _config;

        public UserService(UserRepository userRepo, IMapper mapper, IConfiguration config)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _config = config;
        }

        //SignUp
         public async Task<UserReadDto?> CreateOneAsync(UserCreateDto createDto)
        {
            var foundUser = await _userRepo.FindByEmailAsync(createDto.Email);
            if (foundUser != null)
            {
                throw new ArgumentException("User already exists.");
            }

            PasswordUtils.HashPassword(createDto.Password, out string hashedPassword, out byte[] salt);
            var user = _mapper.Map<UserCreateDto, Users>(createDto);
            user.Password = hashedPassword;
            user.Salt = salt;
            user.Role = createDto.Role ?? Role.Customer;

            var createdUser = await _userRepo.CreateOnAsync(user);
            return _mapper.Map<Users, UserReadDto>(createdUser);
        }

       public async Task<string> LogInAsync(UserLoginDto loginDto)
        {
            var foundUser = await _userRepo.FindByEmailAsync(loginDto.Email);
            if (foundUser == null)
            {
                return "User not found.";
            }

            var passwordMatched = PasswordUtils.VerifyPassword(loginDto.Password, foundUser.Password, foundUser.Salt);
            if (!passwordMatched)
            {
                return "Unauthorized: Incorrect password.";
            }
 var tokenUtil = new TokenUtils(_config);
            return tokenUtil.GenerateToken(foundUser);
        }

         public async Task<bool> DeleteOneAsync(Guid userId)
        {
            var foundUser = await _userRepo.GetByIdAsync(userId);
            if (foundUser == null)
            {
                throw new ArgumentException("User not found.");
            }

            return await _userRepo.DeleteOnAsync(foundUser);
        }

        public async Task<List<UserReadDto>> GetAllAsync()
        {
            var userList = await _userRepo.GetAllAsync();
            return _mapper.Map<List<Users>, List<UserReadDto>>(userList);
        }

        public async Task<UserReadDto> GetByIdAsync(Guid userId)
        {
            var foundUser = await _userRepo.GetByIdAsync(userId);
            return _mapper.Map<Users, UserReadDto>(foundUser);
        }

        public async Task<UserProfileDto> GetProfileIdAsync(Guid userId)
        {
            var foundUser = await _userRepo.GetByIdAsync(userId);
            return _mapper.Map<Users, UserProfileDto>(foundUser);
        }

        public async Task<UserProfileDto> UpdateOneAsync(Guid userId, UserProfileDto updateDto)
        {
            var foundUser = await _userRepo.GetByIdAsync(userId);

            if (foundUser == null)
            {
                return null;
            }

            // Update the user's information with the new data
            foundUser.Name = updateDto.Name;
            foundUser.Email = updateDto.Email;

            // Check if the password needs to be updated
            if (!string.IsNullOrEmpty(updateDto.Password))
            {
                // Hash the new password
                PasswordUtils.HashPassword(
                    updateDto.Password,
                    out string hashedPassword,
                    out byte[] salt
                );
                foundUser.Password = hashedPassword;
                foundUser.Salt = salt;
            }

            await _userRepo.UpdateOnAsync(foundUser);

            return _mapper.Map<Users, UserProfileDto>(foundUser);
        }

        public async Task<bool> UpdatePasswordAsync(Guid userId, PasswordUpdateDto updateDto)
        {
            var foundUser = await _userRepo.GetByIdAsync(userId);

            if (foundUser == null)
            {
                return false; // User not found
            }

            if (!string.IsNullOrEmpty(updateDto.Password))
            {
                // Hash the new password
                PasswordUtils.HashPassword(
                    updateDto.Password,
                    out string hashedPassword,
                    out byte[] salt
                );
                foundUser.Password = hashedPassword;
                foundUser.Salt = salt;
            }

            await _userRepo.UpdateOnAsync(foundUser);

            return true;
        }
    }
}
