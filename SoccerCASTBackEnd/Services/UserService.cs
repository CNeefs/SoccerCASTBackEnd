using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SoccerCASTBackEnd.Data;
using SoccerCASTBackEnd.Helpers;
using SoccerCASTBackEnd.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Services {
    public class UserService : IUserService {
        private readonly AppSettings _appSettings;
        private readonly SoccerContext _soccerContext;

        public UserService(IOptions<AppSettings> appSettings, SoccerContext newsContext) {
            _appSettings = appSettings.Value;
            _soccerContext = newsContext;
        }

        public User Authenticate(string username, string password) {
            var user = _soccerContext.Users.SingleOrDefault(x => x.Email == username);

            // return null if user not found
            if (user == null)
                return null;

            //BCrypt verify
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            user.Roles = _soccerContext.UserRoles.Where(ur => ur.UserID == user.UserID).Include(ur => ur.Role).Select(ur => ur.Role).ToList();


            foreach (Role role in user.Roles)
            {
                user.Permissions = _soccerContext.RolePermissions.Include(rp => rp.Permission).Where(rp => rp.RoleID == role.RoleID).Select(rp => rp.Permission).Select(p => p.Name).Distinct().ToList();
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Email", user.Email),
                    new Claim("BirthDay", user.BirthDate.ToString()),
                    new Claim("TimesLost", user.TimesLost.ToString()),
                    new Claim("TimesWon", user.TimesWon.ToString()),
                    new Claim("Permissions", user.Permissions.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }
    }
}
