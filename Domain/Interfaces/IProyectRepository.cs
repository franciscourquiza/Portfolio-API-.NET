using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProyectRepository : IBaseRepository<Proyect>
    {
        Task<Proyect?> GetByTitle(string title);
        Task Delete(string title);
    }
}
