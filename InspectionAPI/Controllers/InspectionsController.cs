using InspectionAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionsController : ControllerBase
    {
        private readonly DataContext context;

        public InspectionsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inspection>>> GetInspections()
        {
            return await context.inspections.ToListAsync(); ;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inspection>> GetInspections(int id)
        {
            var inspection = await context.inspections.FindAsync(id);

            if(inspection == null)
            {
                return NotFound();
            }

            return inspection;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspection(int id, Inspection inspection)
        {
            if(id != inspection.Id)
            {
                return BadRequest();
            }

            context.Entry(inspection).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionExists(id))
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
        public async Task<ActionResult<Inspection>> PostInspection(Inspection inspection)
        {
            context.inspections.Add(inspection);
            await context.SaveChangesAsync();

            return Ok(await context.inspections.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteInspection(int id)
        {
            var inspection = await context.inspections.FindAsync(id);
            if(inspection == null)
            {
                return NotFound();
            }

            context.inspections.Remove(inspection);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool InspectionExists(int id)
        {
            return context.inspections.Any(e => e.Id == id);
        }
    }
}
