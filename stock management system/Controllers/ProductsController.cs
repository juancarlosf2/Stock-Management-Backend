using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stock_management_system.Data;
using stock_management_system.Models;

namespace stock_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public static IWebHostEnvironment _environment;
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery(Name = "category_id")]int? id, [FromQuery(Name = "search")]string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return await _context.Products.Where(p => p.Name.Contains(searchString)).OrderBy(p => p.Name).ToListAsync();
            }
            switch(id.HasValue){
                case true:
                    {
                        return await _context.Products.Where(p => p.CategoryId == id).OrderBy(product => product.Name).ToListAsync();
                    }
                default: 
                    return await _context.Products.OrderBy(product => product.Name).ToListAsync();
                    
            }

            
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {

            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }


            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, Product product)
        {
            if (id != product.Sku)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Sku))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = product.Sku }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.Sku == id);
        }

        // GET: api/Products/Products-by-Category/1
        /*[HttpGet()]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery(Name = "category_id")]int id)
        {
            var products = await _context.Products.OrderBy(p => p.CategoryId).ToListAsync();
            //.Where(product => product.CategoryId == id).ToListAsync();
            if (products == null)
            {
                return NotFound();
            }
            return products;
        }*/
    }
}
