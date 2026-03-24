namespace Todo.Application.Todos.CreateTodo;

public record CreateTodoCommand(string Title, string? Description, DateTime? DueDate);