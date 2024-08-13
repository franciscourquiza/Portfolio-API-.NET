using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T? Get<TId>(TId id);
        List<T> Get();
        void Add(T entity);
        void Update(T entity);
        void DeleteByEmail<TEmail>(TEmail email);
        Task<bool> Delete(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
