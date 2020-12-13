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
    public class TournamentController : ControllerBase
    {
        private readonly SoccerContext _context;
        public TournamentController(SoccerContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
        {
            return await _context.Tournaments.ToListAsync();


        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();
            return Ok(tournament);
        }

        [Authorize]
        [HttpPost("Start")]
        public async Task<ActionResult<Tournament>> StartTournament(Tournament tournament)
        {
            var teams = _context.TournamentTeams.Where(tt => tt.TournamentID == tournament.TournamentID).ToList();
            
            int count = tournament.Match_Count;
            int rounds = 1;
            for (int i = tournament.Match_Count/2; i > 1; i /= 2) rounds++;

            for (int i = 1; i <= rounds; i++)
            {
                int k = 1;
                for (int j = 1; j <= count / 2; j++)
                {
                    if (i == 1) {
                        var match = new Match();
                        match.Score1 = 0;
                        match.Score2 = 0;
                        match.Date = DateTime.Now;
                        match.TableID = 1;
                        match.MatchTypeID = 1;
                        match.Team1ID = teams[j - 1].TeamID;
                        match.Team2ID = teams[teams.Count - j].TeamID;
                        match.Player1ID = teams[j - 1].Player1ID;
                        match.Player2ID = teams[j - 1].Player2ID;
                        match.Player3ID = teams[teams.Count - j].Player1ID;
                        match.Player4ID = teams[teams.Count - j].Player2ID;
                        match.MatchStatusID = 6;
                        match.TournamentID = tournament.TournamentID;
                        match.Round = i;
                        match.Number = j;
                        match.NextRound = k;
                        _context.Matches.Add(match);
                    } else {
                        var match = new Match();
                        match.Score1 = 0;
                        match.Score2 = 0;
                        match.Date = DateTime.Now;
                        match.TableID = 1;
                        match.MatchTypeID = 1;
                        match.MatchStatusID = 6;
                        match.TournamentID = tournament.TournamentID;
                        match.Round = i;
                        match.Number = j;
                        match.NextRound = k;
                        _context.Matches.Add(match);
                    }
                    if (j % 2 == 0) k++;
                }
                count /= 2;
            }
            tournament.isStart = true;
            _context.Entry(tournament).State = EntityState.Modified;
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

        [Authorize]
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
