using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace TaskManager.Dtos.Tasks;

public record CreateTaskDto
{
    [Required(ErrorMessage = "Task title is required")]
    [StringLength(500, ErrorMessage = "Task title cannot exceed 500 characters")]
    public required string Title { get; init; } = default!;
    [StringLength(500, ErrorMessage = "Task title cannot exceed 500 characters")]
    public string? Description { get; init; }
    [Required]
    [DataType(DataType.DateTime)]
    [Range(typeof(DateTime), "2025-01-01", "2050-01-01", ErrorMessage = "Due date must be between 2025 and 2050")]
    public DateTime DueDate { get; init; }
    [Required(ErrorMessage = "Project selection is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Valid project ID required")]
    public int ProjectId { get; init; }
}