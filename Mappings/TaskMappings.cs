using TaskManager.Dtos.Tasks;
using TaskManager.Models;

namespace TaskManager.Mappings;

public static class TaskMappings
{
    public static TaskDto ToDto(this Models.Task task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        DueDate = task.DueDate,
        IsCompleted = task.IsCompleted,
        ProjectId = task.ProjectId
    };

    public static TaskSummaryDto ToSummaryDto(this Models.Task task) => new()
    {
        Id = task.Id,
        Title = task.Title,
        Description = task.Description,
        DueDate = task.DueDate,
        IsCompleted = task.IsCompleted,
        ProjectId = task.ProjectId
    };

    public static Models.Task ToEntity(this CreateTaskDto dto) =>
        new(dto.Title, dto.Description, dto.DueDate, dto.ProjectId);

    public static void UpdateWith(this Models.Task task, UpdateTaskDto dto)
    {
        task.Title = dto.Title;
        task.Description = dto.Description;
        task.DueDate = dto.DueDate;
        task.IsCompleted = dto.IsCompleted;
    }
}