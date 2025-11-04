using TaskManager.Dtos.Projects;
using TaskManager.Models;

namespace TaskManager.Mappings;

public static class ProjectMappings
{
    public static ProjectDto ToDto(this Project project) => new()
    {
        Id = project.Id,
        Name = project.Name,
        Description = project.Description
    };

    public static ProjectSummaryDto ToSummaryDto(this Project project) => new()
    {
        Id = project.Id,
        Name = project.Name,
        Description = project.Description
    };

    public static Project ToEntity(this CreateProjectDto dto) => new(dto.Name, dto.Description)
    {
        Name = dto.Name,
        Description = dto.Description
    };

    public static void UpdateWith(this Project project, UpdateProjectDto dto)
    {
        project.Name = dto.Name;
        project.Description = dto.Description;
    }
}