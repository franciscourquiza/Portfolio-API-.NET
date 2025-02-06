using AutoMapper;
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
    public class WorkExperienceRepository : BaseRepository<WorkExperience>, IWorkExperienceRepository
    {
        private readonly ApplicationContext _context;
        public WorkExperienceRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        public async Task<WorkExperience?> GetByTitle(string title)
        {
            return await _context.WorkExperiences.FirstOrDefaultAsync(x => x.Title == title);
        }
        public async Task Delete(string title)
        {
            WorkExperience? workExperienceToDelete = await GetByTitle(title);
            if (workExperienceToDelete == null) 
            {
                throw new ArgumentNullException(nameof(workExperienceToDelete));
            }
            _context.WorkExperiences.Remove(workExperienceToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
