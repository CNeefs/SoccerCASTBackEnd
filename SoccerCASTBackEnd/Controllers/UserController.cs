﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SoccerCASTBackEnd.Data;
using SoccerCASTBackEnd.Models;
using SoccerCASTBackEnd.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private IUserService _userService;
        private readonly SoccerContext _context;
        private IBlobService _blobService;

        public UserController(IUserService userService, SoccerContext context, IBlobService blobService) {
            _userService = userService;
            _context = context;
            _blobService = blobService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            foreach(var user in users)
            {
                user.Roles = await _context.UserRoles.Where(ur => ur.UserID == user.UserID).Include(ur => ur.Role).Select(ur => ur.Role).ToListAsync();
            }
            return await _context.Users.ToListAsync();
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            var users = await _context.Users.ToListAsync();
            foreach(User checkUser in users)
            {
                if (checkUser.Email.Equals(user.Email))
                {
                    return BadRequest(new { message = "This email is already in use." });
                }
            }
            user.ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp";          
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            if (user.Roles != null)
            {
                foreach (var role in user.Roles)
                {
                    UserRole userRole = new UserRole();
                    userRole.UserRoleID = 0;
                    userRole.UserID = user.UserID;
                    userRole.RoleID = role.RoleID;
                    _context.UserRoles.Add(userRole);
                }
            }
            await _context.SaveChangesAsync();

            user.Roles = await _context.UserRoles.Where(ur => ur.UserID == user.UserID).Include(ur => ur.Role).Select(ur => ur.Role).ToListAsync();

            foreach (Role role in user.Roles)
            {
                user.Permissions = await _context.Permissions.Where(rp => rp.RoleID == role.RoleID).Select(p => p.Name).Distinct().ToListAsync();
            }
            return Ok(user);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] User userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }
            if (user.UserStatusID == 2)
            {
                return BadRequest(new { message = "Disbanded account" });
            }

            user.Roles = await _context.UserRoles.Where(ur=>ur.UserID == user.UserID).Include(ur => ur.Role).Select(ur => ur.Role).ToListAsync();

            foreach (Role role in user.Roles)
            {
                user.Permissions = await _context.Permissions.Where(rp => rp.RoleID == role.RoleID).Select(p=>p.Name).Distinct().ToListAsync();
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }
            var thisUser = _context.Users.Find(id);
            if (!user.Email.Equals(thisUser.Email))
            {
                var users = await _context.Users.ToListAsync();
                foreach (User checkUser in users)
                {
                    if (checkUser.Email.Equals(user.Email))
                    {
                        return BadRequest(new { message = "This email is already in use." });
                    }
                }
            }
            _context.Entry(thisUser).State = EntityState.Detached;

            _context.UserRoles.RemoveRange(_context.UserRoles.Where(ur => ur.UserID == user.UserID).ToList());
            foreach (var role in user.Roles)
            {
                UserRole userRole = new UserRole();
                userRole.UserRoleID = 0;
                userRole.UserID = user.UserID;
                userRole.RoleID = role.RoleID;
                _context.UserRoles.Add(userRole);
            }
            _context.SaveChanges();
            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
            var user = _context.Users.Find(id);
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
            user.ImagePath = toReturn;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserID == id);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.UserStatusID = 2;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var userteams = _context.UserTeam.Where(ut => ut.UserID == id).ToList();
            foreach (var userteam in userteams)
            {
                _context.UserTeam.Remove(userteam);
            }
            _context.SaveChanges();
            var captainTeams = _context.Teams.Where(t => t.CaptainID == id).ToList();
            foreach (var captainteam in captainTeams)
            {
                captainteam.TeamStatusID = 4;
                _context.Entry(captainteam).State = EntityState.Modified;
                var userteamscaptain = _context.UserTeam.Where(ut => ut.TeamID == captainteam.TeamID).ToList();
                foreach (var userteamcaptain in userteamscaptain)
                {
                    _context.UserTeam.Remove(userteamcaptain);
                }
                _context.SaveChanges();
            }
            return user;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Roles = await _context.UserRoles.Where(ur => ur.UserID == user.UserID).Include(ur => ur.Role).Select(ur => ur.Role).ToListAsync();

            foreach (Role role in user.Roles)
            {
                user.Permissions = await _context.Permissions.Where(rp => rp.RoleID == role.RoleID).Select(p => p.Name).Distinct().ToListAsync();
            }

            return user;
        }
    }
}
