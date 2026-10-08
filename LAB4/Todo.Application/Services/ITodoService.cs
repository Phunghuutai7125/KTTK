namespace Todo.Application.Services
{
    public interface ITodoService
    {
        Task<List<Todo.Domain.Todo>> GetAll();
        Task<Todo.Domain.Todo?> GetById(int id);
        Task AddTodo(Todo.Domain.Todo todo);
        Task UpdateTodo(Todo.Domain.Todo todo);
        Task DeleteTodo(int id);
    }
}