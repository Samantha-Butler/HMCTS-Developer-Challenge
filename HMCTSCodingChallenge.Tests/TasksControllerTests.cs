using Microsoft.VisualStudio.TestTools.UnitTesting;
using HMCTSCodingChallenge.API.Models;
using HMCTSCodingChallenge.API.Data;
using TaskApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMCTSCodingChallenge.Tests;

[TestClass]
public class TasksControllerTests
{
    private TaskDbContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TaskDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
            .Options;

        return new TaskDbContext(options);
    }

    [TestMethod]
    public async Task Create_ShouldReturnCreatedResult()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new TasksController(context);
        var task = new TaskModel
        {
            Title = "Test Task",
            Description = "Test Desc",
            Status = "Pending",
            DueDate = System.DateTime.UtcNow
        };

        // Act
        var result = await controller.Create(task) as CreatedAtActionResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(201, result.StatusCode);
        Assert.IsInstanceOfType(result.Value, typeof(TaskModel));
    }

    [TestMethod]
    public async Task GetTaskId_ExistingId_ReturnsOkResult()
    {
        // Arrange
        var context = GetInMemoryContext();
        var task = new TaskModel { Title = "Test", Status = "Pending", DueDate = System.DateTime.UtcNow };
        context.Tasks.Add(task);
        context.SaveChanges();

        var controller = new TasksController(context);

        // Act
        var result = await controller.GetTaskId(task.Id) as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(200, result.StatusCode);
        Assert.AreEqual(task.Id, ((TaskModel)result.Value!).Id);
    }

    [TestMethod]
    public async Task GetTaskId_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new TasksController(context);

        // Act
        var result = await controller.GetTaskId(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }

    [TestMethod]
    public async Task GetAllTasks_ReturnsAllTasks()
    {
        // Arrange
        var context = GetInMemoryContext();
        context.Tasks.AddRange(new List<TaskModel>
        {
            new TaskModel { Title = "Task1", Status = "Pending", DueDate = System.DateTime.UtcNow },
            new TaskModel { Title = "Task2", Status = "Completed", DueDate = System.DateTime.UtcNow }
        });
        context.SaveChanges();

        var controller = new TasksController(context);

        // Act
        var result = await controller.GetAllTasks() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        var tasks = result.Value as List<TaskModel>;
        Assert.AreEqual(2, tasks!.Count);
    }

    [TestMethod]
    public async Task Update_ValidUpdate_ReturnsNoContent()
    {
        // Arrange
        var context = GetInMemoryContext();
        var task = new TaskModel { Title = "ToUpdate", Status = "Pending", DueDate = DateTime.UtcNow };
        context.Tasks.Add(task);
        context.SaveChanges();

        context.Entry(task).State = EntityState.Detached;

        var controller = new TasksController(context);
        var updatedTask = new TaskModel
        {
            Id = task.Id,
            Title = "Updated Title",
            Status = "Completed",
            DueDate = task.DueDate
        };

        // Act
        var result = await controller.Update(task.Id, updatedTask);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        var saved = context.Tasks.Find(task.Id);
        Assert.AreEqual("Updated Title", saved!.Title);
    }

    [TestMethod]
    public async Task Update_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new TasksController(context);
        var task = new TaskModel { Id = 1, Title = "Mismatch", Status = "Pending", DueDate = System.DateTime.UtcNow };

        // Act
        var result = await controller.Update(999, task);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
    }

    [TestMethod]
    public async Task Delete_ExistingTask_ReturnsNoContent()
    {
        // Arrange
        var context = GetInMemoryContext();
        var task = new TaskModel { Title = "ToDelete", Status = "Pending", DueDate = System.DateTime.UtcNow };
        context.Tasks.Add(task);
        context.SaveChanges();

        var controller = new TasksController(context);

        // Act
        var result = await controller.Delete(task.Id);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        Assert.IsNull(context.Tasks.Find(task.Id));
    }

    [TestMethod]
    public async Task Delete_NonExistingTask_ReturnsNotFound()
    {
        // Arrange
        var context = GetInMemoryContext();
        var controller = new TasksController(context);

        // Act
        var result = await controller.Delete(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundResult));
    }
}
