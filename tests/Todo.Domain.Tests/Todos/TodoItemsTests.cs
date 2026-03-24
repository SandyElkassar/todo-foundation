using FluentAssertions;
using Todo.Domain.ToDos;

namespace Todo.Domain.Tests.ToDos;

public class TodoItemsTests
{
    [Fact]
    public void Constructor_ShouldThrow_WhenTitleIsEmpty()
    {
        Action act = () => new TodoItem("", null, null);
        
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Title is required.*")
            .WithParameterName("title");
    }
    
    [Fact]
    public void Constructor_ShouldThrow_WhenTitleIsTooLong()
    {
        Action act = () => new TodoItem(new string('a', 201), null, null);
        
        act.Should()
            .Throw<ArgumentException>()
            .WithMessage("Title must be 200 characters or fewer. (Parameter 'title')")
            .WithParameterName("title");
    }
    
    [Fact]
    public void Constructor_ShouldSetDefaults_WhenInputIsValid()
    {
        var todo = new TodoItem(new string('a', 14), "description", null);
        
        todo.Id.Should().NotBeEmpty();
        todo.Description.Should().Be("description");
        todo.Title.Length.Should().Be(14);
        todo.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        todo.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));
        todo.IsDone.Should().BeFalse();
    }
}