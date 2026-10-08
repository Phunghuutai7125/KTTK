using Todo.Domain.Data;
using Todo.Domain.Repositories;

namespace Todo.Infrastructure.Repositories
{
    public class TodoRepository : Repository<Todo.Domain.Todo>, ITodoRepository
    {
        public TodoRepository(TodoDbContext context) : base(context)
        {
        }
    }
}