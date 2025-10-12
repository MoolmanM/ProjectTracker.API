using TaskManager.Dtos.Base;

namespace TaskManager.Dtos.Projects;

public record ProjectSummaryDto : BaseDto
{
    public string Name { get; init; } = default!;
    public string? Description { get; init; }
}