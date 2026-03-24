namespace Todo.Application.Todos.CreateTodo;

public record CreateTodoResult(Guid Id, string Title, string? Description, bool IsDone, DateTime CreatedAt, DateTime UpdatedAt, DateTime? DueDate);