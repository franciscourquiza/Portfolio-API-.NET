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
        Education? GetByTitle(string title);
        void Delete(string title);
    }
}
