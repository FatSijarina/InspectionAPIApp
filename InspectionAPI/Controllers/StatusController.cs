using InspectionAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly DataContext context;

        public StatusController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            return await context.Statuses.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatuses(int id)
        {
            var status = await context.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            context.Entry(status).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(Status status)
        {
            context.Statuses.Add(status);
            await context.SaveChangesAsync();

            return Ok(await context.Statuses.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusType(int id)
        {
            var status = await context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            context.Statuses.Remove(status);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(int id)
        {
            return context.Statuses.Any(e => e.Id == id);
        }
    }
}