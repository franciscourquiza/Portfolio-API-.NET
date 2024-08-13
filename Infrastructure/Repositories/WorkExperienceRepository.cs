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
        public WorkExperience? GetByTitle(string title)
        {
            return _context.WorkExperiences.FirstOrDefault(x => x.Title == title);
        }
        public void Delete(string title)
        {
            WorkExperience? workExperienceToDelete = GetByTitle(title);
            if (workExperienceToDelete == null) 
            {
                throw new ArgumentNullException(nameof(workExperienceToDelete));
            }
            _context.WorkExperiences.Remove(workExperienceToDelete);
            _context.SaveChanges();
        }
    }
}
