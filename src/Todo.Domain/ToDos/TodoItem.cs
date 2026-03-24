namespace Todo.Domain.ToDos;

public class TodoItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public bool IsDone { get; private set; }
    public DateTime? DueDate { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public TodoItem(string title, string? description = null, DateTime? dueDate = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.", nameof(title));

        if (title.Length > 200)
            throw new ArgumentException("Title must be 200 characters or fewer.", nameof(title));

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsDone = false;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}