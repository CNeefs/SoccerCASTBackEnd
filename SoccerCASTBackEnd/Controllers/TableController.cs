using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerCASTBackEnd.Data;
using SoccerCASTBackEnd.Models;
using SoccerCASTBackEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase {

        private readonly SoccerContext _context;

        public TableController(SoccerContext context) {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Table>>> GetTables()
        {
            var tables = await _context.Tables.ToListAsync();
            foreach (var table in tables)
            {
                table.ContactUser = _context.Users.Where(x => x.UserID == table.ContactUserID).FirstOrDefault();
                table.ContactUser.Password = "";
            }
            return tables;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Table>> GetTable(int id)
        {
            var table = await _context.Tables.SingleOrDefaultAsync(u => u.TableID == id);
            table.ContactUser = _context.Users.Where(x => x.UserID == table.ContactUserID).FirstOrDefault();
            if (table == null)
            {
                return NotFound();
            }
            return table;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Table>> PostTable(Table table)
        {
            _context.Tables.Add(table);
            await _context.SaveChangesAsync();

            return Ok(table);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(int id, Table table)
        {
            if (id != table.TableID)
            {
                return BadRequest();
            }

            _context.Entry(table).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TableExists(id))
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Table>> DeleteTable(int id)
        {
            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            table.TableStatusID = 2;
            _context.Entry(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return table;
        }

        private bool TableExists(int id)
        {
            return _context.Tables.Any(t => t.TableID == id);
        }
    }
}
