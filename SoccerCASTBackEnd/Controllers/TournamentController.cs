using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerCASTBackEnd.Data;
using SoccerCASTBackEnd.Models;

namespace SoccerCASTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly SoccerContext _context;
        public TournamentController(SoccerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            return await _context.Tournaments.ToListAsync();


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _context.Tournaments.SingleOrDefaultAsync(t => t.TournamentID == id);
            if (tournament == null)
            {
                return NotFound();
            }
            return tournament;
        }

        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();
            return Ok(tournament);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tournament>> DeleteTournament(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return tournament;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Tournament>> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.TournamentID)
            {
                return BadRequest();
            }

            _context.Entry(tournament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
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

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(c => c.TournamentID == id);
        }
    }
}
