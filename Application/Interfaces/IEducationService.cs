using Application.Dtos.EducationDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEducationService
    {
        Task<Education?> GetByTitle(string title);
        Task<Education?> GetById(int id);
        Task<List<Education>> Get();
        Task<Education> Add(EducationForAddDto request, string userEmail);
        Task<Education?> Update(EducationForEditDto request, string title, string userEmail);
        Task Delete(string title, string userEmail);
    }
}
