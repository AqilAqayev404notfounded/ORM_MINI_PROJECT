using ClosedXML.Excel;
using ORM_MINI_PROJECT.DTOs;
using ORM_MINI_PROJECT.Excaption;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Implementations;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Services.Interfances;
using System.Text.RegularExpressions;

namespace ORM_MINI_PROJECT.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService()
    {
        _userRepository = new UserRepository();
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Fulname = u.Fulname,
            Email = u.Email,
            Password = u.Password,
            Address = u.Address
        }).ToList();
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetSingleAsync(u => u.Id == id);
        if (user == null) throw new NotFoundException("User not found");

        return new UserDto
        {
            Id = user.Id,
            Fulname = user.Fulname,
            Email = user.Email,
            Password = user.Password,
            Address = user.Address
        };
    }

    public async Task CreateUserAsync(DTOs.UserDto newUser)
    {
        if (string.IsNullOrWhiteSpace(newUser.Email))
            throw new InvalidUserInformationException("email  bos olanmaz");

        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        if (!regex.IsMatch(newUser.Email))
            throw new InvalidUserInformationException("email yazilsi səhvdir");

        if (string.IsNullOrWhiteSpace(newUser.Password))
            throw new InvalidUserInformationException("Parol bos olanmaz");

        if (!newUser.Password.Any(Char.IsDigit))
            throw new InvalidUserInformationException("password must contain at least one digit");

        if (newUser.Password.Length < 8)
            throw new InvalidUserInformationException("Parolun uzunlugu minimum 8 olmalıdır");

        var isExist = await _userRepository.IsExistAsync(x => x.Email.ToLower() == newUser.Email.ToLower());
        if (isExist == true)
            throw new InvalidUserInformationException("Users cannot have same email");
        var user = new Models.User
        {
            Fulname = newUser.Fulname,
            Email = newUser.Email,
            Password = newUser.Password,
            Address = newUser.Address
        };
        

        await _userRepository.CreateAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(DTOs.UserDto updatedUser)
    {
        var user = await _userRepository.GetSingleAsync(u => u.Id == updatedUser.Id);
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
        var user = await _userRepository.GetSingleAsync(u => u.Id == id);
        if (user == null) throw new NotFoundException("User not found");

        _userRepository.Delete(user);
        await _userRepository.SaveChangesAsync();
    }

    public async Task<UserDto> Login(userPostDto userPostDto)
    {

        var user = await _userRepository.GetSingleAsync(x => x.Email == userPostDto.Email);

        if (user is null)
            throw new Exception("Wrong email or password");

        if (user.Password != userPostDto.Password)
            throw new Exception("Wrong email or password");

        UserDto userDto = new UserDto { Id=user.Id,Email=user.Email,
        Password=user.Password,
        Fulname=user.Fulname,
        Address=user.Address};

        return userDto;


    }
    //public void ExportUserOrdersToExcel(int userId)
    //{
    //    var orders = GetUserOrders(userId);

    //    var workbook = new XLWorkbook();
    //    var worksheet = workbook.Worksheets.Add("User Orders");

    //    worksheet.Cell(1, 1).Value = "Order ID";
    //    worksheet.Cell(1, 2).Value = "Order Date";
    //    worksheet.Cell(1, 3).Value = "Total Amount";
    //    worksheet.Cell(1, 4).Value = "Status";

    //    int row = 2;
    //    foreach (var order in orders)
    //    {
    //        worksheet.Cell(row, 1).Value = order.Id;
    //        worksheet.Cell(row, 2).Value = order.OrderDate;
    //        worksheet.Cell(row, 3).Value = order.TotalAmount;
    //        worksheet.Cell(row, 4).Value = order.Status;
    //        row++;
    //    }

    //    workbook.SaveAs($"User_{userId}_Orders.xlsx");
    //}
        //foreach (var item in await GetAllUsersAsync())
        //{


        //    if(email == item.Email&& pasworrd == item.Password)
        //    {
        //        i = true; break;
        //    }

        //}

}
