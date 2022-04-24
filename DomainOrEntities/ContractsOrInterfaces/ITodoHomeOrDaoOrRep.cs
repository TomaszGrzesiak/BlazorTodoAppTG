// using System.Collections.Generic;
// using System.Threading.Tasks;

using DomainOrEntities.Models;

namespace DomainOrEntities.ContractsOrInterfaces;

public interface ITodoHomeOrDaoOrRep
{
    public Task<ICollection<Todo>> GetAsync();
    public Task<Todo> GetById(int id);
    public Task<Todo> AddAsync(Todo todo);
    public Task DeleteAsync(int id);
    public Task UpdateAsync(Todo todo);
}