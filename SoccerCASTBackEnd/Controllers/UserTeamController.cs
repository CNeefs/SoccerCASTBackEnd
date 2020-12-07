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
    public class UserTeamController : ControllerBase
    {
        private readonly SoccerContext _context;
        public UserTeamController(SoccerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetUserTeams()
        {
            return await _context.UserTeam.ToListAsync();


        }
        [HttpGet("/UserTeams{id}")]
        public async Task<ActionResult<IEnumerable<Team>>> GetUserTeams(int id)
        {
            List<Team> teams = null;
            var userteams = await _context.UserTeam.Where(ut => ut.UserID == id).ToListAsync();
            if (userteams == null)
            {
                return NotFound();
            }

            foreach(var userteam in userteams)
            {
                Team tempTeam = await _context.Teams.SingleOrDefaultAsync(teams => teams.TeamID == userteam.TeamID);
                teams.Add(tempTeam);
            }

            return await teams;
            
            
        }

        [HttpPost]
        public async Task<ActionResult<UserTeam>> PostUserTeam(UserTeam userteam)
        {
            _context.UserTeam.Add(userteam);
            await _context.SaveChangesAsync();
            return Ok(userteam);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserTeam>> DeleteUserTeam(int id)
        {
            var userteam = await _context.UserTeam.FindAsync(id);
            if (userteam == null)
            {
                return NotFound();
            }

            _context.UserTeam.Remove(userteam);
            await _context.SaveChangesAsync();
            return userteam;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserTeam>> PutUserTeam(int id, UserTeam userteam)
        {
            if (id != userteam.UserTeamID)
            {
                return BadRequest();
            }

            _context.Entry(userteam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTeamExists(id))
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

        private bool UserTeamExists(int id)
        {
            return _context.UserTeam.Any(c => c.UserTeamID == id);
        }
    }
}
