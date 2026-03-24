namespace Todo.Application.Todos.CreateTodo;

public interface ICreateTodoHandler
{
    public Task<CreateTodoResult> HandleAsync(CreateTodoCommand request, CancellationToken cancellationToken);
}