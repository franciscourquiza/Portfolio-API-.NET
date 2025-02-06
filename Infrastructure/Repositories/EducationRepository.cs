using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EducationRepository : BaseRepository<Education>, IEducationRepository
    {
        private readonly ApplicationContext _context;
        public EducationRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Education?> GetByTitle(string title)
        {
            return await _context.Educations.FirstOrDefaultAsync(e => e.Title == title);
        }
        public async Task Delete(string title)
        {
            Education? educationToDelete = await GetByTitle(title);
            if (educationToDelete == null)
            {
                throw new ArgumentNullException(nameof(educationToDelete));
            }
            _context.Educations.Remove(educationToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
