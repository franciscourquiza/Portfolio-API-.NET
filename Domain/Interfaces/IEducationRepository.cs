using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEducationRepository : IBaseRepository<Education>
    {
        Task<Education?> GetByTitle(string title);
        Task Delete(string title);
    }
}
