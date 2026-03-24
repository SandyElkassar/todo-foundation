using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Todo.Domain.ToDos;

namespace Todo.Infrastructure.Persistence.Configurations;

public class TodoItemConfiguration: IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.ToTable("TodoItems");

        builder.HasKey(item => item.Id);

        builder.Property(item => item.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(item => item.Description)
            .IsRequired(false);

        builder.Property(item => item.DueDate)
            .IsRequired(false);
    }
}
