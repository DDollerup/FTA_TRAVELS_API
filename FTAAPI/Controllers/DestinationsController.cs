#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FTAAPI.Models;

namespace FTAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly FTAContext _context;
        private readonly IWebHostEnvironment _env;

        public DestinationsController(FTAContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Destinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Destination>>> GetDestinations()
        {
            return await _context.Destinations.Include(e => e.DestinationImages).ToListAsync();
        }

        // GET: api/Destinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Destination>> GetDestination(int id)
        {
            var destination = await _context.Destinations.Include(e => e.DestinationImages).SingleOrDefaultAsync(e => e.Id == id);

            if (destination == null)
            {
                return NotFound();
            }

            return destination;
        }

        // PUT: api/Destinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestination(int id, Destination destination)
        {
            if (id != destination.Id)
            {
                return BadRequest();
            }

            if (Tools.IsBase64String(destination.Image))
            {
                Destination oldDestination = await _context.Destinations.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
                if (!string.IsNullOrEmpty(oldDestination.Image))
                {
                    System.IO.File.Delete(_env.ContentRootPath + @"\wwwroot\" + destination.Image);
                }
                destination.Image = Tools.ConvertBase64ToFile(destination.Image, _env.ContentRootPath + @"\wwwroot\");
            }

            if (destination?.DestinationImages?.Count > 0)
            {
                foreach (var item in destination.DestinationImages)
                {
                    item.Image = Tools.ConvertBase64ToFile(item.Image, _env.ContentRootPath + @"\wwwroot\");
                }
            }

            _context.Entry(destination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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

        // POST: api/Destinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Destination>> PostDestination(Destination destination)
        {
            destination.Image = Tools.ConvertBase64ToFile(destination.Image, _env.ContentRootPath + @"\wwwroot\");
            if (destination?.DestinationImages?.Count > 0)
            {
                foreach (var item in destination.DestinationImages)
                {
                    item.Image = Tools.ConvertBase64ToFile(item.Image, _env.ContentRootPath + @"\wwwroot\");
                }
            }
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestination", new { id = destination.Id }, destination);
        }

        // DELETE: api/Destinations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestination(int id)
        {
            var destination = await _context.Destinations.Include(e => e.DestinationImages).SingleOrDefaultAsync(e => e.Id == id);
            if (destination == null)
            {
                return NotFound();
            }
            System.IO.File.Delete(_env.ContentRootPath + @"\wwwroot\" + destination.Image);
            if (destination?.DestinationImages?.Count > 0)
            {
                foreach (var item in destination.DestinationImages)
                {
                    System.IO.File.Delete(_env.ContentRootPath + @"\wwwroot\" + item.Image);
                }
            }

            _context.Destinations.Remove(destination);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestinationExists(int id)
        {
            return _context.Destinations.Any(e => e.Id == id);
        }
    }
}
