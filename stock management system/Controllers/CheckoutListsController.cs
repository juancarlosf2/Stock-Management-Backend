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
    public class CheckoutListsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CheckoutListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CheckoutLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckoutList>>> GetCheckoutLists()
        {
            return await _context.CheckoutLists.ToListAsync();
        }

        // GET: api/CheckoutLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckoutList>> GetCheckoutList(int id)
        {
            var checkoutList = await _context.CheckoutLists.FindAsync(id);

            if (checkoutList == null)
            {
                return NotFound();
            }

            return checkoutList;
        }

        // PUT: api/CheckoutLists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckoutList(int id, CheckoutList checkoutList)
        {
            if (id != checkoutList.CheckoutId)
            {
                return BadRequest();
            }

            _context.Entry(checkoutList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckoutListExists(id))
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

        // POST: api/CheckoutLists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CheckoutList>> PostCheckoutList(CheckoutList checkoutList)
        {
            _context.CheckoutLists.Add(checkoutList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CheckoutListExists(checkoutList.CheckoutId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCheckoutList", new { id = checkoutList.CheckoutId }, checkoutList);
        }

        // DELETE: api/CheckoutLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CheckoutList>> DeleteCheckoutList(int id)
        {
            var checkoutList = await _context.CheckoutLists.FindAsync(id);
            if (checkoutList == null)
            {
                return NotFound();
            }

            _context.CheckoutLists.Remove(checkoutList);
            await _context.SaveChangesAsync();

            return checkoutList;
        }

        private bool CheckoutListExists(int id)
        {
            return _context.CheckoutLists.Any(e => e.CheckoutId == id);
        }
    }
}
