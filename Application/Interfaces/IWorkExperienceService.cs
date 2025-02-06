using Application.Dtos.AdminDtos;
using Application.Dtos.WorkExperienceDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IWorkExperienceService
    {
        Task<WorkExperience?> Get(string title);
        Task<WorkExperience?> Get(int id);
        Task<List<WorkExperience>> Get();
        Task<WorkExperience?> AddWorkExperience(WorkExperienceForAdd request, string userEmail);
        Task<WorkExperience?> Update(WorkExperienceForEditDto request, string title, string userEmail);
        Task Delete(string title, string userEmail);
    }
}
