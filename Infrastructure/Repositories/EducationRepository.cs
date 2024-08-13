using Domain.Entities;
using Domain.Interfaces;
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
        public Education? GetByTitle(string title)
        {
            return _context.Educations.FirstOrDefault(e => e.Title == title);
        }
        public void Delete(string title)
        {
            Education? educationToDelete = GetByTitle(title);
            if (educationToDelete == null)
            {
                throw new ArgumentNullException(nameof(educationToDelete));
            }
            _context.Educations.Remove(educationToDelete);
            _context.SaveChanges();
        }
    }
}
