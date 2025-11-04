using TaskManager.Dtos.Base;

namespace TaskManager.Dtos.Tasks;

public record TaskSummaryDto : BaseDto
{
    public string Title { get; init; } = default!;
    public string? Description { get; init; }
    public DateTime DueDate { get; init; }
    public bool IsCompleted { get; init; }
    public int ProjectId { get; init; }
}