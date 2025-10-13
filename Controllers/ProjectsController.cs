using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Construction;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Dtos.Projects;
using TaskManager.Mappings;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/Projects - Return summary list for performance
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.Projects
                .Select(p => p.ToSummaryDto())
                .ToListAsync();

            return Ok(projects);
        }

        // GET: api/Projects/5 - Return full project details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            return Ok(project.ToDto());
        }

        // POST: api/Projects - Create from DTO
        [HttpPost]
        public async Task<IActionResult> CreateProject(CreateProjectDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var project = createDto.ToEntity();
            if (project == null) return BadRequest();

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                actionName: nameof(GetProject),
                routeValues: new { id = project.Id },
                value: project.ToDto());
        }

        // PUT: api/Projects/5 - Update with DTO
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, UpdateProjectDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            project.UpdateWith(updateDto);
            await _context.SaveChangesAsync();

            return Ok(project.ToSummaryDto());
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return NotFound();

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
