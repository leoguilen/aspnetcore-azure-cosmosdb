using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> FindByIdAsync(Guid id);
        Task<bool> CreateAsync(T obj);
        Task<bool> UpdateAsync(Guid id, T obj);
        Task<bool> DeleteAsync(Guid id);
    }
}