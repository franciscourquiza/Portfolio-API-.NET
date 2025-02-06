using Application.Dtos.AdminDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task<Admin?> GetAdminByName(string name);
        Task<Admin?> GetAdminForCreation(string email);
        Task<Admin?> GetAdminByEmail(string email);
        Task<List<Admin>> GetAllAdmins();
        Task<Admin> AddAdmin(AdminForAddDto request);
        Task<Admin> UpdateAdmin(AdminForEditDto request, string email);
        Task DeleteAdminByEmail(string email);
    }
}
