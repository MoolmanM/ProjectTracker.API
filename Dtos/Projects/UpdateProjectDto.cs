using System.ComponentModel.DataAnnotations;

namespace TaskManager.Dtos.Projects;

public record UpdateProjectDto
{
    [Required(ErrorMessage = "Project name is required")]
    [StringLength(100, ErrorMessage = "Project name cannot exceed 100 characters")]
    public required string Name { get; init; }


    [StringLength(500, ErrorMessage = "Project description cannot exceed 500 characters")]
    public string? Description { get; init; }
}