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
        Admin? GetByEmail(string email);
        Admin? Get(string name);
        void AddAdmin(User user);
    }
}
