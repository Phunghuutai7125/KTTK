using Todo.Domain.Repositories;

namespace Todo.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Domain.Todo>> GetAll()
        {
            var todos = await _repository.GetAllAsync();
            return todos.ToList();
        }

        public async Task<Domain.Todo?> GetById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddTodo(Domain.Todo todo)
        {
            await _repository.AddAsync(todo);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateTodo(Domain.Todo todo)
        {
            var item = await _repository.GetByIdAsync(todo.Id);
            if (item != null)
            {
                item.Title = todo.Title;
                item.IsCompleted = todo.IsCompleted;
                await _repository.SaveChangesAsync();
            }
        }

        public async Task DeleteTodo(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo != null)
            {
                _repository.DeleteAsync(todo);
                await _repository.SaveChangesAsync();
            }
        }
    }
}