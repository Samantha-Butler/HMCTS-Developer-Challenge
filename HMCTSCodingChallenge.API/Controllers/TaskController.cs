using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HMCTSCodingChallenge.API.Models;
using HMCTSCodingChallenge.API.Data;

namespace TaskApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly TaskDbContext _context;

        public TasksController(TaskDbContext context)
        {
            _context = context;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskModel task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTaskId), new { id = task.Id }, task);
        }
        
        // READ by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskId(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task == null ? NotFound() : Ok(task);
        }
        
        // Get all tasks
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskModel updatedTask)
        {
            if (id != updatedTask.Id) return BadRequest();

            _context.Entry(updatedTask).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
