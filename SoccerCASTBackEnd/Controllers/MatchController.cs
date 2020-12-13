using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return Ok(match);
        }

        [Authorize]
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

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Match>> PutMatch(int id, Match match)
        {
            if(id != match.MatchID)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;
            _context.SaveChanges();

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
                //Check for next match
                if (match.TournamentID != null)
                {
                    if (_context.Matches.Where(m => m.TournamentID == match.TournamentID && m.Round == (match.Round + 1)).Count() != 0) {
                        var nextMatch = _context.Matches.Where(m => m.TournamentID == match.TournamentID && m.Round == (match.Round + 1) &&
                            m.Number == match.NextRound).SingleOrDefault();
                        var previousMatches = _context.Matches.Where(m => m.TournamentID == match.TournamentID && m.Round == match.Round && m.NextRound == match.NextRound).ToList();
                        var arrayNumber1 = 1;
                        var arrayNumber2 = 0;
                        if (previousMatches[0].MatchID == match.MatchID) {
                            arrayNumber1 = 0;
                            arrayNumber2 = 1;
                        }
                        if (previousMatches[arrayNumber1].Number < previousMatches[arrayNumber2].Number) {
                            if (match.Score1 > match.Score2) {
                                nextMatch.Team1ID = match.Team1ID;
                                nextMatch.Player1ID = match.Player1ID;
                                nextMatch.Player2ID = match.Player2ID;
                            } else {
                                nextMatch.Team1ID = match.Team2ID;
                                nextMatch.Player1ID = match.Player3ID;
                                nextMatch.Player2ID = match.Player4ID;
                            }
                        } else {
                            if (match.Score1 > match.Score2) {
                                nextMatch.Team2ID = match.Team1ID;
                                nextMatch.Player3ID = match.Player1ID;
                                nextMatch.Player4ID = match.Player2ID;
                            } else {
                                nextMatch.Team2ID = match.Team2ID;
                                nextMatch.Player3ID = match.Player3ID;
                                nextMatch.Player4ID = match.Player4ID;
                            }
                        }
                        _context.Entry(nextMatch).State = EntityState.Modified;
                    } else {
                        var tournament = _context.Tournaments.Where(t => t.TournamentID == match.TournamentID).SingleOrDefault();
                        tournament.isStart = false;
                        if (match.Score1 > match.Score2) {
                            var name = _context.Teams.Where(t => t.TeamID == match.Team1ID).SingleOrDefault().TeamName;
                            tournament.Winner = name;
                        } else {
                            var name = _context.Teams.Where(t => t.TeamID == match.Team2ID).SingleOrDefault().TeamName;
                            tournament.Winner = name;
                        }
                        _context.Entry(tournament).State = EntityState.Modified;
                        _context.SaveChanges();
                    }
                }
            }

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
