using Microsoft.AspNetCore.Mvc;
using TaskManager.Dtos.Tasks;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Mappings;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks
                .Select(t => t.ToSummaryDto())
                .ToListAsync();

            return Ok(tasks);
        }

        //GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int Id)
        {
            var task = await _context.Tasks.FindAsync(Id);

            if (task == null) return NotFound();

            return Ok(task.ToDto());
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskDto createDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var task = createDto.ToEntity();
            if (task == null) return BadRequest();

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                actionName: nameof(GetTask),
                routeValues: new { id = task.Id },
                value: task.ToDto());
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, UpdateTaskDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.UpdateWith(updateDto);
            await _context.SaveChangesAsync();

            return Ok(task.ToSummaryDto());
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
