using ORM_MINI_PROJECT.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORM_MINI_PROJECT.Services.Interfances;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task CreateUserAsync(UserDto newUser);
    Task UpdateUserAsync(UserDto updatedUser);
    Task DeleteUserAsync(int id);
    Task<UserDto> GetUserByIdAsync(int id);
}
