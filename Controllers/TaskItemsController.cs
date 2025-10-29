using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCollabTool.Data;
using ProjectCollabTool.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCollabTool.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // <-- මේකෙ URL එක /api/TaskItems වෙයි
    public class TaskItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskItems
        // (tasks ඔක්කොම ගන්න - හැබැයි tasks ගොඩක් උනොත් මේක හොඳ නෑ, 
        //  ඒත් පටන් ගන්න හොඳයි)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            return await _context.TaskItems.ToListAsync();
        }

        // GET: api/TaskItems/5
        // (ID එක දීලා, එක task එකක් විතරක් ගන්න)
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        // POST: api/TaskItems
        // (අලුතෙන් task එකක් හදන method එක)
        // මේකට JSON body එකක් එවන්න ඕන projectId එකත් එක්කම
        // උදා: { "title": "Do database", "projectId": 1 }
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        {
            // අමතරව: project එකක් තියෙනවද කියලත් බලන්න පුළුවන්
            var projectExists = await _context.Projects.AnyAsync(p => p.Id == taskItem.ProjectId);
            if (!projectExists)
            {
                // projectId එක වැරදි නම්, error එකක් යවනවා
                return BadRequest("Invalid Project ID");
            }

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
        }

        // PUT: api/TaskItems/5
        // (Task එකක් update කරන්න. උදා: complete කරන්න)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(int id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest("ID in URL does not match ID in body");
            }

            // task එක update කරනවා කියලා EF එකට කියනවා
            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // task එක database එකේ නැත්නම්
                if (!_context.TaskItems.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // 204 status - සාර්ථකයි, content එකක් නෑ
        }

        // DELETE: api/TaskItems/5
        // (Task එකක් delete කරන්න)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 status
        }
    }
}
