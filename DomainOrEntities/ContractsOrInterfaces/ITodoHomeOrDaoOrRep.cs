// using System.Collections.Generic;
// using System.Threading.Tasks;

using DomainOrEntities.Models;

namespace DomainOrEntities.ContractsOrInterfaces;

public interface ITodoHomeOrDaoOrRep
{
    public Task<ICollection<Todo>> GetAsync();
    public Task<ICollection<Todo>> GetAsync(int? userId, bool? isCompleted);
    public Task<Todo> GetByIdAsync(int id);
    public Task<Todo> AddAsync(Todo todo);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(Todo todo);
}