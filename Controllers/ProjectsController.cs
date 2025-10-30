using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // <-- මේක වැදගත්
using ProjectCollabTool.Data;
using ProjectCollabTool.Models;
using System.Collections.Generic;
using System.Linq; // <-- මේකත් add කරගන්න
using System.Threading.Tasks;

namespace ProjectCollabTool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // === මෙන්න වෙනස් කරපු තැන ===
        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            // අපි Projects ගේනකොට, ඒවට අදාළ Tasks ලැයිස්තුවත්
            // "Include" කරලා ගේන්න කියලා කියනවා.
            var projects = await _context.Projects
                .Include(p => p.Tasks) // <-- මේක තමා අලුත් line එක
                .ToListAsync();
            
            return projects;
        }
        // === වෙනස ඉවරයි ===

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            // එක project එකක් ගේනකොටත් Tasks ටික Include කරමු
            var project = await _context.Projects
                .Include(p => p.Tasks) // <-- මෙතනටත් ඒ line එක දැම්මා
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }

        // POST: api/Projects
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
            // DateCreated එක server-side එකෙන් set කරන එක වඩා හොඳයි
            project.DateCreated = DateTime.UtcNow;

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            // CreatedAtAction එකෙන් කියන්නෙ "ID 1න් හැදුවා" වගේ response එකක්
            // ඒකට "GetProject" (උඩ තියෙන) method එක පාවිච්චි කරනවා
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
    }
}

