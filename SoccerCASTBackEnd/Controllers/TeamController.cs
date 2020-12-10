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
    public class TeamController : ControllerBase
    {
        private readonly SoccerContext _context;
        public TeamController(SoccerContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await _context.Teams.Include(t => t.Captain).Include(t => t.TeamStatus).ToListAsync();
            return teams;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.Include(t => t.Captain).Include(t => t.TeamStatus).SingleOrDefaultAsync(t => t.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }
            return team;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            return Ok(team);
        }

        [HttpPost("join/{id}")]
        public async Task<ActionResult<Team>> JoinTeam(int id, Team team)
        {
            UserTeam userTeam = new UserTeam();
            userTeam.TeamID = team.TeamID;
            userTeam.UserID = id;
            userTeam.UserTeamStatusID = 1;
            _context.UserTeam.Add(userTeam);
            await _context.SaveChangesAsync();
            return Ok(team);
        }

        [HttpPost("join/review/{id}")]
        public async Task<ActionResult<Team>> JoinReviewTeam(int id, Team team)
        {
            UserTeam userTeam = new UserTeam();
            userTeam.TeamID = team.TeamID;
            userTeam.UserID = id;
            userTeam.UserTeamStatusID = 2;
            _context.UserTeam.Add(userTeam);
            await _context.SaveChangesAsync();
            return Ok(team);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return team;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Team>> PutTeam(int id, Team team)
        {
            if (id != team.TeamID)
            {
                return BadRequest();
            }

            _context.Entry(team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamExists(id))
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

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(c => c.TeamID == id);
        }
    }
}
