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
    public class CompetitionController : ControllerBase
    {
        private readonly SoccerContext _context;
        public CompetitionController(SoccerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
        {
            return await _context.Competitions.ToListAsync();


        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Competition>> GetCompetition(int id)
        {
            var competition =  await _context.Competitions.SingleOrDefaultAsync(c => c.CompetitionID == id);
            if(competition == null)
            {
                return NotFound();
            }
            return competition;
        }

        [HttpPost]
        public async Task<ActionResult<Competition>> PostCompetition(Competition competition)
        {
            _context.Competitions.Add(competition);
            await _context.SaveChangesAsync();
            return Ok(competition);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Competition>> DeleteCompetition(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }

            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();
            return competition;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Competition>> PutCompetition(int id, Competition competition)
        {
            if (id != competition.CompetitionID)
            {
                return BadRequest();
            }

            _context.Entry(competition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(id))
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

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(c => c.CompetitionID == id);
        }

        [HttpPut("active/{id}")]
        public async Task<ActionResult<Competition>> SetActive(int id, Competition competition)
        {
            if (id != competition.CompetitionID)
            {
                return BadRequest();
            }

            var competitions = await _context.Competitions.Where(c => c.isActive == true).Where(c => c.CompetitionID != id).ToListAsync();
            foreach (var deactivateCompetition in competitions)
            {
                deactivateCompetition.isActive = false;
                _context.Entry(deactivateCompetition).State = EntityState.Modified;
            }

            _context.Entry(competition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(id))
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
    }
}
