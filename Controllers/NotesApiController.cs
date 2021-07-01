using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FBA2.Data;
using FBA2.Models;

namespace FBA2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesApiController : ControllerBase
    {
        private readonly Database2Context _context;

        public NotesApiController(Database2Context context)
        {
            _context = context;
        }

        // GET: api/NotesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notes>>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        // GET: api/NotesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notes>> GetNotes(int id)
        {
            var notes = await _context.Notes.FindAsync(id);

            if (notes == null)
            {
                return NotFound();
            }

            return notes;
        }

        // PUT: api/NotesApi/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotes(int id, Notes notes)
        {
            if (id != notes.Id)
            {
                return BadRequest();
            }

            _context.Entry(notes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotesExists(id))
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

        // POST: api/NotesApi
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Notes>> PostNotes(Notes notes)
        {
            _context.Notes.Add(notes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NotesExists(notes.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNotes", new { id = notes.Id }, notes);
        }

        // DELETE: api/NotesApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Notes>> DeleteNotes(int id)
        {
            var notes = await _context.Notes.FindAsync(id);
            if (notes == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(notes);
            await _context.SaveChangesAsync();

            return notes;
        }

        private bool NotesExists(int id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}
