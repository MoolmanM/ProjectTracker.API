using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos.Tasks;

public record UpdateTaskDto
{
    [Required(ErrorMessage = "Task title is required")]
    [StringLength(500, ErrorMessage = "Task title cannot exceed 500 characters")]
    public required string Title { get; init; }
    [StringLength(500, ErrorMessage = "Task title cannot exceed 500 characters")]
    public string? Description { get; init; }
    public bool IsCompleted { get; init; }
    [Required]
    [DataType(DataType.DateTime)]
    [Range(typeof(DateTime), "2025-01-01", "2050-01-01", ErrorMessage = "Due date must be between 2025 and 2050")]
    public DateTime DueDate { get; init; }
}