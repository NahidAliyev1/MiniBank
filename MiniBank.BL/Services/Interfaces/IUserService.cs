using MiniBank.BL.DTOS;
using MiniBank.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.BL.Services.Interfaces;
public interface IUserService
{
    Task<List<User>> GetAllUsersAsync();
   Task<User> GetUserByIdAsync(int id);
    Task CreateUserAsync(CreateUserDto user);
    Task UpdateUserAsync(int id,User user);
    Task DeleteUserAsync(User user);
    Task<User> GetUserWithCardsAsync(int userId);

}
