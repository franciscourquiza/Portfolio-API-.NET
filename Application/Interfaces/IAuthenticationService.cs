using Application.Dtos.AuthDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<Tuple<bool, User?>> ValidateUser(string email, string password);
        Task<bool> RequestResetPassword(ResetPasswordRequest request);
        Task<bool> ResetPassword(string token);
    }
}
