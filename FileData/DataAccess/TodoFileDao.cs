using DomainOrEntities.ContractsOrInterfaces;
using DomainOrEntities.Models;

namespace FileData.DataAccess;

public class TodoFileDao : ITodoHomeOrDaoOrRep
{
    private FileContext fileContext;

    public TodoFileDao(FileContext fileContext)
    {
        this.fileContext = fileContext;
    }

    public async Task<ICollection<Todo>> GetAsync()
    {
        ICollection<Todo> todos = fileContext.Todos;
        return todos;
    }

    public async Task<Todo> GetByIdAsync(int id)
    {
        // return first "toodo" in the collection, which id equals to the "id" from arguments of this method  
        return fileContext.Todos.First(t => t.Id == id);
    }

    public async Task<Todo> AddAsync(Todo todo)
    {
        int largestId = 0;
        if (fileContext.Todos.Count != 0) largestId = fileContext.Todos.Max(t => t.Id);
        todo.Id = largestId + 1;
        fileContext.Todos.Add(todo);
        fileContext.SaveChanges();
        return todo;
    }

    public async Task DeleteAsync(int id)
    {
        Todo todoToDelete = fileContext.Todos.First(t => t.Id == id);
        fileContext.Todos.Remove(todoToDelete);
        fileContext.SaveChanges();
    }
    
    public Task UpdateAsync(Todo todo)
    {
        Todo todoToUpdate = fileContext.Todos.First(t => t.Id == todo.Id);
        // Troels' solution
        todoToUpdate.IsCompleted = todo.IsCompleted;
        todoToUpdate.OwnerId = todo.OwnerId;
        todoToUpdate.Title = todo.Title;
        fileContext.SaveChanges();
        return Task.CompletedTask;

        // below: my solutions
        // fileContext.Todos.Remove(todoToUpdate);
        // fileContext.Todos.Add(todo);
        // fileContext.SaveChanges();
    }

    public Task<ICollection<Todo>> GetAsync(int? userId, bool? isCompleted)
    {
        throw new NotImplementedException();
    }
}