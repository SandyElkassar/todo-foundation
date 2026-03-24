using Todo.Application.Todos.CreateTodo;
using Todo.Domain.ToDos;
using Todo.Infrastructure.Persistence;

namespace Todo.Infrastructure.Todos;

public class CreateTodoHandler(AppDbContext db) : ICreateTodoHandler
{
    public async Task<CreateTodoResult> HandleAsync(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var item = new TodoItem(request.Title, request.Description, request.DueDate);
        await db.TodoItems.AddAsync(item, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return new CreateTodoResult(item.Id, item.Title, item.Description, item.IsDone, item.CreatedAt, item.UpdatedAt, item.DueDate);
    }
}