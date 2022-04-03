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
    public class InspectionTypesController : ControllerBase
    {
        private readonly DataContext context;

        public InspectionTypesController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspectionType>>> GetInspectionTypes()
        {
            return await context.InspectionTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InspectionType>> GetInspectionTypes(int id)
        {
            var inspection = await context.InspectionTypes.FindAsync(id);

            if (inspection == null)
            {
                return NotFound();
            }

            return inspection;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspection(int id, InspectionType inspectionType)
        {
            if (id != inspectionType.Id)
            {
                return BadRequest();
            }

            context.Entry(inspectionType).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionTypeExists(id))
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
        public async Task<ActionResult<List<InspectionType>>> PostInspectionType(InspectionType inspectiontype)
        {
            context.InspectionTypes.Add(inspectiontype);
            await context.SaveChangesAsync();

            return Ok(await context.InspectionTypes.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspectionType(int id)
        {
            var inspectiontype = await context.InspectionTypes.FindAsync(id);
            if (inspectiontype == null)
            {
                return NotFound();
            }

            context.InspectionTypes.Remove(inspectiontype);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool InspectionTypeExists(int id)
        {
            return context.InspectionTypes.Any(e => e.Id == id);
        }
    }
}