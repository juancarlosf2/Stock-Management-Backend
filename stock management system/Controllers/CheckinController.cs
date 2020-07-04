using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stock_management_system.Data;
using stock_management_system.Models;

namespace stock_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckinController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CheckinController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Checkin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Checkin>>> GetCheckin()
        {
            return await _context.Checkin.ToListAsync();
        }

        // GET: api/Checkin/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Checkin>> GetCheckin(int id)
        {
            var checkin = await _context.Checkin.FindAsync(id);

            if (checkin == null)
            {
                return NotFound();
            }

            return checkin;
        }

        // PUT: api/Checkin/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckin(int id, Checkin checkin)
        {
            if (id != checkin.Id)
            {
                return BadRequest();
            }

            _context.Entry(checkin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckinExists(id))
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

        // POST: api/Checkin
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Checkin>> PostCheckin(Checkin checkin)
        {
            _context.Checkin.Add(checkin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckin", new { id = checkin.Id }, checkin);
        }

        // DELETE: api/Checkin/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Checkin>> DeleteCheckin(int id)
        {
            var checkin = await _context.Checkin.FindAsync(id);
            if (checkin == null)
            {
                return NotFound();
            }

            _context.Checkin.Remove(checkin);
            await _context.SaveChangesAsync();

            return checkin;
        }

        private bool CheckinExists(int id)
        {
            return _context.Checkin.Any(e => e.Id == id);
        }
    }
}
