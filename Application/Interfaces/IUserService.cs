using Application.Dtos.UserDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserWithoutPasswordDto> GetUserWithoutPasswordByName(string name);
        Task<UserWithoutPasswordDto> GetUserWithoutPasswordByEmail(string email);
        Task<List<User>> GetAllUsers();
        Task<User> CreateUser(UserForAddRequest request);
        Task<User> UpdateUser(UserForEditDto request, string userEmail);
        Task DeleteUserByEmail(string email);
    }
}
