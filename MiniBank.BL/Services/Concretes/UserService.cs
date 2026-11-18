using Microsoft.EntityFrameworkCore;
using MiniBank.BL.DTOS;
using MiniBank.BL.Services.Interfaces;
using MiniBank.DAL.Entities;
using MiniBank.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.BL.Services.Concretes;
public class UserService : IUserService
{
    private readonly IRepository<User> _repository;

    public UserService(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task CreateUserAsync(CreateUserDto dto)
    {
        if (dto != null)
        {
            User newUser = new User
            {
                FullName = dto.FullName,
                FinCode = dto.FinCode,
                PhoneNumber = dto.PhoneNumber
            };

            await _repository.CreateAsync(newUser);
            await _repository.SaveChangesAsync();
        }
        else
        {
            throw new Exception("User məlumatları boş ola bilməz");
        }
    }

    public async Task DeleteUserAsync(User user)
    {
        if (user!=null)
        {
             _repository.DeleteAsync(user);
          await  _repository.SaveChangesAsync();
        }
        else
        {
          throw new Exception("User melumatlarinda yanlisklik olub");
        }
       
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        User user = await _repository.GetByIdAsync(id);
        if (user !=null)
        {
            return user;
        }
        else
        {
            throw new Exception("İstifadəçi tapılmadı");
        }
    }

    public async Task<User> GetUserWithCardsAsync(int userId)
    {
        User? user = await _repository.GetAllQuery()
            .Include(u => u.Cards)
            .FirstOrDefaultAsync(u => u.Id == userId);
        if (user==null)
        {
            throw new Exception("İstifadəçi tapılmadı");
        }
        return user;
        
    }

    public async Task UpdateUserAsync(int id,User user)
    {
        if (user == null)
            throw new Exception("Məlumatlar boş ola bilməz");

        var oldUser = await _repository.GetByIdAsync(id);
        if (oldUser == null)
            throw new Exception("Yenilənəcək istifadəçi tapılmadı");

        oldUser.FullName = user.FullName;
        oldUser.FinCode = user.FinCode;
        oldUser.PhoneNumber = user.PhoneNumber;

        _repository.UpdateAsync(id,oldUser);
        await _repository.SaveChangesAsync();


    }

   
}
