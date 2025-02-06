using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<Admin?> GetByName(string name);
        Task<Admin?> GetByEmail(string email);
    }
}
