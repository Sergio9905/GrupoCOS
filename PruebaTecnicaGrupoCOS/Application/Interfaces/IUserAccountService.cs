using PruebaTecnicaGrupoCOS.Application.DTOs;
using PruebaTecnicaGrupoCOS.Application.Responses;
using PruebaTecnicaGrupoCOS.Core.Entities;

namespace PruebaTecnicaGrupoCOS.Application.Interfaces
{
    public interface IUserAccountService
    {
        Task<IEnumerable<UserAccountResponse>> GetAllUsersAsync();
        Task<UserAccountResponse> GetUserByIdAsync(int id);
        Task<UserAccountResponse> CreateUserAsync(UserAccountDto userDto);
        Task<bool> UpdateUserAsync(int id, UserAccountDto userDto);
        Task<bool> DeleteUserAsync(int id);
    }
}
