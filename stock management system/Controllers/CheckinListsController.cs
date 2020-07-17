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
    public class CheckinListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CheckinListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CheckinLists    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckinList>>> GetCheckinLists()
        {
            return await _context.CheckinLists.ToListAsync();
        }

        // GET: api/CheckinLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckinList>> GetCheckinList(int id)
        {
            var checkinList = await _context.CheckinLists.FindAsync(id);

            if (checkinList == null)
            {
                return NotFound();
            }

            return checkinList;
        }

        // PUT: api/CheckinLists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckinList(int id, CheckinList checkinList)
        {
            if (id != checkinList.Id)
            {
                return BadRequest();
            }

            _context.Entry(checkinList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckinListExists(id))
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

        // POST: api/CheckinLists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CheckinList>> PostCheckinList(CheckinList checkinList)
        {
            _context.CheckinLists.Add(checkinList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCheckinList", new { id = checkinList.Id }, checkinList);
        }

        // DELETE: api/CheckinLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CheckinList>> DeleteCheckinList(int id)
        {
            var checkinList = await _context.CheckinLists.FindAsync(id);
            if (checkinList == null)
            {
                return NotFound();
            }

            _context.CheckinLists.Remove(checkinList);
            await _context.SaveChangesAsync();

            return checkinList;
        }

        private bool CheckinListExists(int id)
        {
            return _context.CheckinLists.Any(e => e.Id == id);
        }
    }
}
