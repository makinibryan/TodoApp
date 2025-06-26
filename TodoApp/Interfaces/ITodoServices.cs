using TodoApp.Contracts;
using TodoApp.Models;

namespace TodoApp.Interfaces
{
    public interface ITodoServices
    {
        Task<IEnumerable<Todo>> GetAllAsync();
        Task<Todo> GetByIdAsync(Guid id);
        Task CreateTodoAsync(CreateTodoRequest request);
        Task UpdateTodoAsync(Guid id, UpdateTodoRequest request);
        Task DeleteTodoAsync(Guid id);
    }
}
