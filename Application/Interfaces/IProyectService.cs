using Application.Dtos.ProyectDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProyectService
    {
        Task<Proyect?> GetByTitle(string title);
        Task<Proyect?> Get(int id);
        Task<List<Proyect>> Get();
        Task<Proyect> Add(ProyectForAddDto request, string userEmail);
        Task<Proyect> Update(ProyectForEditDto request, string title, string userEmail);
        Task Delete(string title, string userEmail);
    }
}
