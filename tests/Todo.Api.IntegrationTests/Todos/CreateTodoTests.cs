using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Todo.Api.IntegrationTests.Infrastructure;
using Todo.Infrastructure.Persistence;

namespace Todo.Api.IntegrationTests.Todos;

public class CreateTodoTests(TodoApiFactoryFixture factory) : IClassFixture<TodoApiFactoryFixture>
{
    [Theory]
    [InlineData("/todos")]
    public async Task Post_Todo_ReturnsCreated(string url)
    {
        // Arrange
        var request = new
        {
            title = "Buy milk",
            description = "2 liters",
            dueDate = (DateTime?)null
        };

        // Act
        var response = await factory.Client.PostAsJsonAsync(url, request);

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}