using AutoMapper;
using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Interfaces;
using PruebaTecnicaGrupoCOS.Application.Responses;
using PruebaTecnicaGrupoCOS.Core.Entities;
using PruebaTecnicaGrupoCOS.Core.Interfaces;
using PruebaTecnicaGrupoCOS.Helper;

namespace PruebaTecnicaGrupoCOS.Application.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserAccountRepository _userRepository;
        private readonly IMapper _mapper;

        public UserAccountService(IUserAccountRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserAccountResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserAccountResponse>>(users);
        }

        public async Task<UserAccountResponse> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return _mapper.Map<UserAccountResponse>(user);
        }

        public async Task<UserAccountResponse> CreateUserAsync(UserAccountDto userDto)
        {
            if (await _userRepository.GetByEmailAsync(userDto.Email) != null)
                throw new ApplicationException("Email already exists");

            if (await _userRepository.GetByUserNameAsync(userDto.UserName) != null)
                throw new ApplicationException("Username already exists");

            var user = _mapper.Map<UserAccount>(userDto);
            user.Password = AuthHelper.HashPassword(userDto.Password);

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserAccountResponse>(user);
        }

        public async Task<bool> UpdateUserAsync(int id, UserAccountDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            var existingEmail = await _userRepository.GetByEmailAsync(userDto.Email);
            if (existingEmail != null && existingEmail.Id != id)
                throw new ApplicationException("Email already exists");

            var existingUsername = await _userRepository.GetByUserNameAsync(userDto.UserName);
            if (existingUsername != null && existingUsername.Id != id)
                throw new ApplicationException("Username already exists");

            _mapper.Map(userDto, user);

            if (!string.IsNullOrEmpty(userDto.Password))
                user.Password = AuthHelper.HashPassword(userDto.Password);

            _userRepository.Update(user);
            return await _userRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found");

            _userRepository.Delete(user);
            return await _userRepository.SaveChangesAsync();
        }
    }
}
