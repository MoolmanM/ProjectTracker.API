using TaskManager.Dtos.Base;

namespace TaskManager.Dtos.Projects;

public record ProjectDto : BaseDto
{
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
}