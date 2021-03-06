﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerCASTBackEnd.Data;
using SoccerCASTBackEnd.Models;
using SoccerCASTBackEnd.Services;

namespace SoccerCASTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly SoccerContext _context;
        public IBlobService _blobService;
        public TeamController(SoccerContext context, IBlobService blobService)
        {
            _context = context;
            _blobService = blobService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = await _context.Teams.Include(t => t.Captain).Include(t => t.TeamStatus).ToListAsync();
            foreach (var team in teams)
            {
                team.Users = await _context.UserTeam.Where(ut => ut.TeamID == team.TeamID && ut.UserTeamStatusID == 1).Include(ut => ut.User).Select(ut => ut.User).ToListAsync();
            }
            return teams;
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            team.ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-team-picture.jpg";
            _context.Teams.Add(team);
            await _context.SaveChangesAsync();
            UserTeam userTeam = new UserTeam();
            userTeam.TeamID = team.TeamID;
            userTeam.UserID = team.CaptainID;
            userTeam.UserTeamStatusID = 1;
            _context.UserTeam.Add(userTeam);
            await _context.SaveChangesAsync();
            return Ok(team);
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            team.TeamStatusID = 4;
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var userteams = _context.UserTeam.Where(ut => ut.TeamID == id).ToList();
            foreach(var userteam in userteams)
            {
                _context.UserTeam.Remove(userteam);
            }
            _context.SaveChanges();
            return team;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Team>> PutTeam(int id, Team team)
        {
            if (id != team.TeamID)
            {
                return BadRequest();
            }

            if (team.TeamStatusID == 1)
            {
                var userteams = _context.UserTeam.Where(ut => ut.TeamID == team.TeamID).ToList();
                foreach (var userteam in userteams)
                {
                    userteam.UserTeamStatusID = 1;
                    _context.Entry(userteam).State = EntityState.Modified;
                }
            }

            if (team.TeamStatusID == 3)
            {
                _context.UserTeam.RemoveRange(_context.UserTeam.Where(ut => ut.TeamID == team.TeamID && ut.UserTeamStatusID == 2));
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

        [Authorize]
        [HttpPost("{id}/upload"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadProfilePicture(int id)
        {
            var team = _context.Teams.Find(id);
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest();
            }

            var fileName = Guid.NewGuid().ToString() + file.FileName;

            var result = await _blobService.UploadFileBlobAsync(
                    "firstcontainer",
                    file.OpenReadStream(),
                    file.ContentType,
                    fileName);

            var toReturn = result.AbsoluteUri;
            team.ImagePath = toReturn;

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

            return Ok(new { path = toReturn });
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(c => c.TeamID == id);
        }
    }
}
