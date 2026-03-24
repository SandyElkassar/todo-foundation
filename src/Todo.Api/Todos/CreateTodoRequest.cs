namespace Todo.Api.Todos;

public class CreateTodoRequest
{
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public DateTime? DueDate { get; set; }
}