using SoccerCASTBackEnd.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Models {
    public class DBInitializer {
        public static void Initialize(SoccerContext context) {
            context.Database.EnsureCreated();

            // Look for users
            if (context.Users.Any()) {
                return;   // DB has been seeded
            }

            context.Roles.AddRange(
                new Role { Name = "User" },
                new Role { Name = "Admin" }
            );
            context.SaveChanges();

            context.Users.AddRange(
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Player", LastName = "1", Email = "player1@thomasmore.be", BirthDate = new DateTime(1969,5,4), RoleID = 1, TimesLost=0, TimesWon=0 },
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Player", LastName = "2", Email = "player2@thomasmore.be", BirthDate = new DateTime(1969, 11, 21), RoleID = 1, TimesLost = 0, TimesWon = 0 }
            );

            context.SaveChanges();

            context.Tables.Add(
                new Table { TableName = "Tafel 1", Adres = "Molenstraat 21 2000 Antwerpen", ContactUserID = 1}
                );
            context.SaveChanges();

            context.MatchTypes.AddRange(
                new MatchType { TypeName="1v1"},
                new MatchType { TypeName = "2v2" }
                );
            context.SaveChanges();

            context.MatchStatuses.AddRange(
                new MatchStatus { StatusName = "Planned"},
                new MatchStatus { StatusName = "Playing" },
                new MatchStatus { StatusName = "Played" },
                new MatchStatus { StatusName = "Cancelled" }
                );
            context.SaveChanges();

            context.Tournaments.Add(
                new Tournament { Edition = "Doomsday 2020", Match_Count = 16}
                );
            context.SaveChanges();

            context.Competitions.Add(
                new Competition { Name="Eerste klasse", isActive = true}
                );
            context.SaveChanges();

            context.Matches.Add(
                new Match { Date = new DateTime(2020,12,27), Score1 = 1, Score2 = 2, TableID = 1, MatchTypeID = 1, CompetitionID = 1, Player1ID = 1, Player2ID = 2, MatchStatusID = 1 }
                );
            context.SaveChanges();

            context.Teams.Add(
                new Team { TeamName = "First team", CaptainID = 1, Location = "Antwerpen", CompanyName = "Testbedrijf"}
                );
            context.SaveChanges();

            context.UserTeam.AddRange(
                new UserTeam { TeamID = 1, UserID = 2},
                new UserTeam { TeamID = 1, UserID = 1}
                );
            context.SaveChanges();
        }
    }
}
