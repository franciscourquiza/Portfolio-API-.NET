using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context) : base(context)
        { 
            _context = context;
        }
        public User? Get(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Name == name);
        }
        public User? GetByEmail(string email)
        {
              return _context.Users.FirstOrDefault(u => u.Email == email);
        }
        public async Task<User> GetByEmailAsync(string email)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                return user;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
