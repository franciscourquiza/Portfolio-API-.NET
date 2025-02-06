using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        private readonly ApplicationContext _context;
        public AdminRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Admin?> GetByName(string name)
        {
            return await _context.Admins.FirstOrDefaultAsync(u => u.Name == name && u.UserRole == "Admin");
        }
        public async Task<Admin?> GetByEmail(string email)
        {
            return await _context.Admins.FirstOrDefaultAsync(u => u.Email == email && u.UserRole == "Admin");
        }

    }
}
