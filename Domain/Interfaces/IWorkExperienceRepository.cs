using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IWorkExperienceRepository : IBaseRepository<WorkExperience>
    {
        Task<WorkExperience?> GetByTitle(string title);
        Task Delete(string title);
    }
}
