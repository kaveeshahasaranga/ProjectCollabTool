using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectCollabTool.Data;     // <-- අපේ DbContext එක තියෙන තැන
using ProjectCollabTool.Models;   // <-- අපේ Models තියෙන තැන
using System.Collections.Generic;
using System.Threading.Tasks;

// අපි මේ class එකට namespace එක දෙනවා
namespace ProjectCollabTool.Controllers
{
    // මේ attributes දෙකෙන් කියනවා මේක API Controller එකක් කියලා
    // ඒ වගේම URL එක /api/Projects වෙන්න ඕන කියලා
    [ApiController]
    [Route("api/[controller]")] 
    public class ProjectsController : ControllerBase
    {
        // 1. database connection එක තියාගන්න private variable එකක් හදනවා
        private readonly ApplicationDbContext _context;

        // 2. මේකට තමා "Dependency Injection" කියන්නෙ.
        //    Program.cs එකේ අපි register කරපු DbContext එක,
        //    ASP.NET එකෙන් αυτόματα මේ constructor එක හරහා අපිට දෙනවා.
        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 3. GET: /api/Projects
        //    (projects ඔක්කොම return කරන method එක)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            // database එකේ (Projects table එකේ) තියෙන හැම project එකක්ම
            // list එකක් විදිහට අරගන්නවා.
            var projects = await _context.Projects.ToListAsync();
            
            // ඒ list එක return කරනවා (Ok = 200 status)
            return Ok(projects);
        }

        // 4. GET: /api/Projects/5  (උදාහරණයක් විදිහට 5 ගත්තෙ)
        //    (ID එක දීලා, එක project එකක් විතරක් ගන්න method එක)
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            // database එකේ ID එකෙන් project එක හොයනවා
            var project = await _context.Projects.FindAsync(id);

            // project එක හම්බුනේ නැත්නම්, "Not Found" (404) return කරනවා
            if (project == null)
            {
                return NotFound();
            }

            // project එක හම්බුනොත්, ඒක return කරනවා (Ok = 200 status)
            return Ok(project);
        }

        // 5. POST: /api/Projects
        //    (අලුතෙන් project එකක් හදන method එක)
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        { // <-- මේ වරහන මගහැරිලා තිබ්බෙ. මම ඒක add කලා.
            _context.Projects.Add(project); // database එකට project එක add කරනවා
            await _context.SaveChangesAsync(); // database එකේ save කරනවා

            // අලුතෙන් හදපු project එක "201 Created" status එකත් එක්ක
            // return කරනවා. මේක හොඳ practice එකක්.
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }
    }
}

