using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Services.Interfances;

namespace ORM_MINI_PROJECT.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.id,
            Fulname = u.Fulname,
            Email = u.Email,
            Password = u.Password,
            Address = u.Address
        }).ToList();
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetSingleAsync(u => u.id == id);
        if (user == null) throw new NotFoundException("User not found");

        return new UserDto
        {
            Id = user.id,
            Fulname = user.Fulname,
            Email = user.Email,
            Password = user.Password,
            Address = user.Address
        };
    }

    public async Task CreateUserAsync(UserDto newUser)
    {
        var user = new User
        {
            Fulname = newUser.Fulname,
            Email = newUser.Email,
            Password = newUser.Password,
            Address = newUser.Address
        };

        await _userRepository.CreateAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(UserDto updatedUser)
    {
        var user = await _userRepository.GetSingleAsync(u => u.id == updatedUser.Id);
        if (user == null) throw new NotFoundException("User not found");

        user.Fulname = updatedUser.Fulname;
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;
        user.Address = updatedUser.Address;

        _userRepository.Update(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetSingleAsync(u => u.id == id);
        if (user == null) throw new NotFoundException("User not found");

        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();
    }
}
