using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerCASTBackEnd.Data;
using SoccerCASTBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentTeamController : ControllerBase
    {
        private readonly SoccerContext _context;
        public TournamentTeamController(SoccerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TournamentTeam>>> GetTournamentTeams(int id)
        {
            return await _context.TournamentTeams.Where(tt => tt.TournamentID == id).Include(tt => tt.Team).Include(tt => tt.Player1).Include(tt => tt.Player2).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<TournamentTeam>>> AddTournamentTeam(TournamentTeam tournamentTeam)
        {
            var tournament = _context.Tournaments.Where(t => t.TournamentID == tournamentTeam.TournamentID).SingleOrDefault();
            tournament.Total_Joined++;
            _context.Entry(tournament).State = EntityState.Modified;
            _context.TournamentTeams.Add(tournamentTeam);
            await _context.SaveChangesAsync();
            return Ok(tournamentTeam);
        }
    }
}
