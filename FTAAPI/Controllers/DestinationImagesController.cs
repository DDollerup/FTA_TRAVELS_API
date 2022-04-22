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
    public class DestinationImagesController : ControllerBase
    {
        private readonly FTAContext _context;
        private readonly IWebHostEnvironment _env;

        public DestinationImagesController(FTAContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/DestinationImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestinationImage>>> GetDestinationImages()
        {
            return await _context.DestinationImages.ToListAsync();
        }

        // GET: api/DestinationImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DestinationImage>> GetDestinationImage(int id)
        {
            var destinationImage = await _context.DestinationImages.FindAsync(id);

            if (destinationImage == null)
            {
                return NotFound();
            }

            return destinationImage;
        }

        // PUT: api/DestinationImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDestinationImage(int id, DestinationImage destinationImage)
        {
            if (id != destinationImage.Id)
            {
                return BadRequest();
            }

            if (Tools.IsBase64String(destinationImage.Image))
            {
                DestinationImage oldDestinationImage = await _context.DestinationImages.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
                if (!string.IsNullOrEmpty(oldDestinationImage.Image))
                {
                    System.IO.File.Delete(_env.ContentRootPath + @"\wwwroot\" + destinationImage.Image);
                }
                destinationImage.Image = Tools.ConvertBase64ToFile(destinationImage.Image, _env.ContentRootPath + @"\wwwroot\");
            }

            _context.Entry(destinationImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationImageExists(id))
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

        // POST: api/DestinationImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DestinationImage>> PostDestinationImage(DestinationImage destinationImage)
        {
            destinationImage.Image = Tools.ConvertBase64ToFile(destinationImage.Image, _env.ContentRootPath + @"\wwwroot\");
            _context.DestinationImages.Add(destinationImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDestinationImage", new { id = destinationImage.Id }, destinationImage);
        }

        // DELETE: api/DestinationImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDestinationImage(int id)
        {
            var destinationImage = await _context.DestinationImages.FindAsync(id);
            if (destinationImage == null)
            {
                return NotFound();
            }

            System.IO.File.Delete(_env.ContentRootPath + @"\wwwroot\" + destinationImage.Image);

            _context.DestinationImages.Remove(destinationImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DestinationImageExists(int id)
        {
            return _context.DestinationImages.Any(e => e.Id == id);
        }
    }
}
