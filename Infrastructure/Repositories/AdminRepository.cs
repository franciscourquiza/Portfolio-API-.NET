using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        private readonly ApplicationContext _context;
        public AdminRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public Admin? Get(string name)
        {
            return _context.Admins.FirstOrDefault(u => u.Name == name);
        }
        public Admin? GetByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(u => u.Email == email);
        }
        public void AddAdmin(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
