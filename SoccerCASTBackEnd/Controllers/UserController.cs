﻿using Microsoft.AspNetCore.Authorization;
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

        public UserController(IUserService userService, SoccerContext context) {
            _userService = userService;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            foreach(var user in users)
            {
                var userRoles = await _context.UserRoles.Where(x => x.UserID == user.UserID).ToListAsync();
                var roles = new List<int>();
                foreach (var userRole in userRoles)
                {
                    roles.Add(_context.Roles.Where(x => x.RoleID == userRole.RoleID).SingleOrDefault().RoleID);
                }
                user.Roles = await _context.Roles.Where(x => roles.Contains(x.RoleID)).ToListAsync();
            }
            return await _context.Users.ToListAsync();
        }

        [HttpPost]
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

            user.Roles = await _context.UserRoles.Where(ur=>ur.UserID == user.UserID).Include(ur => ur.Role).Select(ur => ur.Role).ToListAsync();


            foreach (Role role in user.Roles)
            {
                user.Permissions = await _context.Permissions.Where(rp => rp.RoleID == role.RoleID).Select(p=>p.Name).Distinct().ToListAsync();
            }


            
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }
            _context.UserRoles.RemoveRange(_context.UserRoles.Where(ur => ur.UserID == user.UserID).ToList());
            foreach (var role in user.Roles)
            {
                UserRole userRole = new UserRole();
                userRole.UserRoleID = 0;
                userRole.UserID = user.UserID;
                userRole.RoleID = role.RoleID;
                _context.UserRoles.Add(userRole);
            }
            await _context.SaveChangesAsync();

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

        private bool UserExists(int id)
        {
            return _context.Users.Any(u => u.UserID == id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.UserRoles.RemoveRange(_context.UserRoles.Where(ur => ur.UserID == user.UserID).ToList());
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

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

            var userRoles = await _context.UserRoles.Where(x => x.UserID == user.UserID).ToListAsync();
            var roles = new List<int>();
            foreach (var userRole in userRoles)
            {
                roles.Add(_context.Roles.Where(x => x.RoleID == userRole.RoleID).SingleOrDefault().RoleID);
            }
            user.Roles = await _context.Roles.Where(x => roles.Contains(x.RoleID)).ToListAsync();

            return user;
        }
    }
}
