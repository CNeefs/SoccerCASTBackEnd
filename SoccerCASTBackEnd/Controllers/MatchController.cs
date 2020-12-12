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
    public class MatchController : ControllerBase
    {
        private readonly SoccerContext _context;

        public MatchController(SoccerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches
                .Include(m => m.Table)
                .Include(m => m.MatchType)
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.MatchStatus)
                .Include(m => m.Competition)
                .Include(m => m.Tournament)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Matches
                .Include(m => m.Table)
                .Include(m => m.MatchType)
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.Player3)
                .Include(m => m.Player4)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.MatchStatus)
                .Include(m => m.Competition)
                .Include(m => m.Tournament)
                .SingleOrDefaultAsync(m => m.MatchID == id);
            if (match == null)
            {
                return NotFound();
            }
            return match;
        }

        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetUserMatches(int id)
        {
            var matches = await _context.Matches.Where(m => m.Player1ID == id || m.Player2ID == id || m.Player3ID == id || m.Player4ID == id)
                .Include(m => m.Table)
                .Include(m => m.MatchType)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.Player3)
                .Include(m => m.Player4)
                .Include(m => m.MatchStatus)
                .Include(m => m.Competition)
                .Include(m => m.Tournament)
                .ToListAsync();
            if (matches == null)
            {
                return NotFound();
            }
            return matches;
        }

        [HttpGet("Team/{id}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetTeamMatches(int id)
        {
            var matches = await _context.Matches.Where(m => m.Team1ID == id || m.Team2ID == id)
                .Include(m => m.Table)
                .Include(m => m.MatchType)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.Player3)
                .Include(m => m.Player4)
                .Include(m => m.MatchStatus)
                .Include(m => m.Competition)
                .Include(m => m.Tournament)
                .ToListAsync();
            if (matches == null)
            {
                return NotFound();
            }
            return matches;
        }

        [HttpGet("Tournament/{id}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetTournamentMatches(int id)
        {
            var matches = await _context.Matches.Where(m => m.TournamentID == id)
                .Include(m => m.Table)
                .Include(m => m.MatchType)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.Player1)
                .Include(m => m.Player2)
                .Include(m => m.Player3)
                .Include(m => m.Player4)
                .Include(m => m.MatchStatus)
                .Include(m => m.Competition)
                .Include(m => m.Tournament)
                .ToListAsync();
            if (matches == null)
            {
                return NotFound();
            }
            return matches;
        }

        [HttpGet("Team/Competitions/{id}")]
        public async Task<ActionResult<IEnumerable<Match>>> GetTeamCompetitions(int id)
        {
            var matches = await _context.Matches.Where(m => m.Team1ID == id || m.Team2ID == id)
                .Include(m => m.Table)
                .Include(m => m.MatchType)
                .Include(m => m.Team1)
                .Include(m => m.Team2)
                .Include(m => m.MatchStatus)
                .Include(m => m.Competition)
                .Include(m => m.Tournament)
                .ToListAsync();
            if (matches == null)
            {
                return NotFound();
            }
            return matches;
        }

        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return Ok(match);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Match>> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if(match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
            return match;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Match>> PutMatch(int id, Match match)
        {
            if(id != match.MatchID)
            {
                return BadRequest();
            }
            if (match.MatchStatusID == 4)
            {
                if (match.Player1ID != null)
                {
                    var user = _context.Users.Where(u => u.UserID == match.Player1ID).SingleOrDefault();
                    if (match.Score1 > match.Score2) user.TimesWon++;
                    else user.TimesLost++;
                    _context.Entry(user).State = EntityState.Modified;
                }
                if (match.Player2ID != null)
                {
                    var user = _context.Users.Where(u => u.UserID == match.Player2ID).SingleOrDefault();
                    if (match.Score1 > match.Score2) user.TimesWon++;
                    else user.TimesLost++;
                    _context.Entry(user).State = EntityState.Modified;
                }
                if (match.Player3ID != null)
                {
                    var user = _context.Users.Where(u => u.UserID == match.Player3ID).SingleOrDefault();
                    if (match.Score1 < match.Score2) user.TimesWon++;
                    else user.TimesLost++;
                    _context.Entry(user).State = EntityState.Modified;
                }
                if (match.Player4ID != null)
                {
                    var user = _context.Users.Where(u => u.UserID == match.Player4ID).SingleOrDefault();
                    if (match.Score1 < match.Score2) user.TimesWon++;
                    else user.TimesLost++;
                    _context.Entry(user).State = EntityState.Modified;
                }
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!MatchExists(id))
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

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(m => m.MatchID == id);
        }
    }
}
