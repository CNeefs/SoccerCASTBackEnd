﻿using SoccerCASTBackEnd.Data;
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

            context.Permissions.AddRange(
                new Permission { Name = "createTable"},
                new Permission { Name = "updateTable" },
                new Permission { Name = "deleteTable" },
                new Permission { Name = "editTable" },
                new Permission { Name = "createMatch" },
                new Permission { Name = "updateMatch" },
                new Permission { Name = "deleteMatch" },
                new Permission { Name = "updateMatch" }
                );
            context.SaveChanges();

            context.RolePermissions.AddRange(
                new RolePermission { PermissionID = 1, RoleID = 2},
                new RolePermission { PermissionID = 2, RoleID = 2 },
                new RolePermission { PermissionID = 3, RoleID = 2 },
                new RolePermission { PermissionID = 4, RoleID = 2 },
                new RolePermission { PermissionID = 5, RoleID = 2 },
                new RolePermission { PermissionID = 6, RoleID = 2 },
                new RolePermission { PermissionID = 7, RoleID = 2 },
                new RolePermission { PermissionID = 8, RoleID = 2 }
                );
            context.SaveChanges();

            context.Users.AddRange(
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Peter", LastName = "Celie", Email = "player1@thomasmore.be", BirthDate = new DateTime(1969,5,4), TimesLost=0, TimesWon=0 },
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Tom", LastName = "Aat", Email = "player2@thomasmore.be", BirthDate = new DateTime(1969, 11, 21), TimesLost = 0, TimesWon = 0 }
            );
            context.SaveChanges();

            context.UserRoles.AddRange(
                new UserRole { UserID = 1, RoleID = 2},
                new UserRole { UserID = 2, RoleID = 1}
                );
            context.SaveChanges();

            context.Tables.Add(
                new Table { TableName = "Tafel 1", Locatie = "Molenstraat 21 2000 Antwerpen", ContactUserID = 1}
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
