using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Todos;
using Todo.Application.Todos.CreateTodo;
using Todo.Infrastructure.Persistence;
using Todo.Infrastructure.Todos;

var builder = WebApplication.CreateBuilder(args);

//register SQLite with the DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICreateTodoHandler, CreateTodoHandler>();

var app = builder.Build();

app.MapPost("/todos",
        async ([FromBody] CreateTodoRequest todoRequest, ICreateTodoHandler handler, CancellationToken ct) =>
        {
            try
            {
                var todoCommand =
                    new CreateTodoCommand(todoRequest.Title, todoRequest.Description, todoRequest.DueDate);
                var result = await handler.HandleAsync(todoCommand, ct);
                return Results.Created($"/todos/{result.Id}", result);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { error = ex.Message });
            }
        }).Produces(StatusCodes.Status201Created)
    .WithName("CreateTodo")
    .Produces(StatusCodes.Status400BadRequest);

app.Run();

public partial class Program;