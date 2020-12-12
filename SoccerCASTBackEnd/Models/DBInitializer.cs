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

            context.Permissions.AddRange(
                new Permission { Name = "TABLE_MANAGE", RoleID = 2 },
                new Permission { Name = "TABLE_CREATE", RoleID = 2 },
                new Permission { Name = "TABLE_EDIT", RoleID = 2 },
                new Permission { Name = "TABLE_VIEW", RoleID = 2 },
                new Permission { Name = "COMPETITION_MANAGE", RoleID = 2 },
                new Permission { Name = "COMPETITION_CREATE", RoleID = 2 },
                new Permission { Name = "COMPETITION_EDIT", RoleID = 2 },
                new Permission { Name = "COMPETITION_VIEW", RoleID = 2 },
                new Permission { Name = "USER_MANAGE", RoleID = 2 },
                new Permission { Name = "USER_CREATE", RoleID = 2 },
                new Permission { Name = "USER_EDIT", RoleID = 2 },
                new Permission { Name = "USER_VIEW", RoleID = 2 },
                new Permission { Name = "TOURNAMENT_MANAGE", RoleID = 2 },
                new Permission { Name = "TOURNAMENT_CREATE", RoleID = 2 },
                new Permission { Name = "TOURNAMENT_EDIT", RoleID = 2 },
                new Permission { Name = "TOURNAMENT_VIEW", RoleID = 2 },
                new Permission { Name = "TOURNAMENT_JOIN", RoleID = 2 },
                new Permission { Name = "TOURNAMENT_JOIN", RoleID = 1 },
                new Permission { Name = "TEAM_MANAGE", RoleID = 2 },
                new Permission { Name = "TEAM_CREATE", RoleID = 2 },
                new Permission { Name = "TEAM_CREATE", RoleID = 1 },
                new Permission { Name = "TEAM_EDIT", RoleID = 2 },
                new Permission { Name = "TEAM_EDIT", RoleID = 1 },
                new Permission { Name = "TEAM_ADD", RoleID = 2 },
                new Permission { Name = "TEAM_ADD", RoleID = 1 },
                new Permission { Name = "TEAM_VIEW", RoleID = 2 },
                new Permission { Name = "TEAM_VIEW", RoleID = 1 },
                new Permission { Name = "PROFILE_VIEW", RoleID = 2 },
                new Permission { Name = "PROFILE_VIEW", RoleID = 1 },
                new Permission { Name = "PROFILE_EDIT", RoleID = 2 },
                new Permission { Name = "PROFILE_EDIT", RoleID = 1 },
                new Permission { Name = "MATCH_START", RoleID = 2 },
                new Permission { Name = "MATCH_START", RoleID = 1 },
                new Permission { Name = "MATCH_CHALLENGE", RoleID = 2 },
                new Permission { Name = "MATCH_CHALLENGE", RoleID = 1 },
                new Permission { Name = "MATCH_ACCEPT", RoleID = 2 },
                new Permission { Name = "MATCH_ACCEPT", RoleID = 1 },
                new Permission { Name = "RANKING_VIEW", RoleID = 2 },
                new Permission { Name = "RANKING_VIEW", RoleID = 1 },
                new Permission { Name = "PROFILE_EDIT", RoleID = 2 },
                new Permission { Name = "PROFILE_EDIT", RoleID = 1 },
                new Permission { Name = "PLAY_VIEW", RoleID = 2 },
                new Permission { Name = "PLAY_VIEW", RoleID = 1 }
            );
            context.SaveChanges();

            context.Users.AddRange(
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Peter", LastName = "Celie", Email = "player1@thomasmore.be", BirthDate = new DateTime(1969,5,4), TimesLost=0, TimesWon=0, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/9f686271-7aa3-415f-81c7-b7a44128b068Peterselie.jpg" },
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Tom", LastName = "Aat", Email = "player2@thomasmore.be", BirthDate = new DateTime(1969, 11, 21), TimesLost = 2, TimesWon = 5, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/64a2f0fc-8b17-4651-ac96-0424403e739btomaat-groente-Veggipedia.png" },
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Apple", LastName = "Sien", Email = "player3@thomasmore.be", BirthDate = new DateTime(1969, 3, 8), TimesLost = 0, TimesWon = 0, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/f14cdf2e-7e32-4836-9133-94ad9185a90976e3afed87dc07327310a312c2c65ffe.jpg" },
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Ban", LastName = "Aan", Email = "player4@thomasmore.be", BirthDate = new DateTime(1969, 9, 28), TimesLost = 0, TimesWon = 0, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/c6c68100-7f89-423d-902b-e00d6877252bbanaan-vierkant.jpg" },
                new User { Password = BCrypt.Net.BCrypt.HashPassword("test"), FirstName = "Ki", LastName = "Wi", Email = "player5@thomasmore.be", BirthDate = new DateTime(1969, 2, 4), TimesLost = 0, TimesWon = 0, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/1ebb586f-0afb-490e-ad1e-baa8cfba52e5Kiwi-fruits-582a07b.jpg" }
            );
            context.SaveChanges();

            context.UserRoles.AddRange(
                new UserRole { UserID = 5, RoleID = 2 },
                new UserRole { UserID = 5, RoleID = 1 },
                new UserRole { UserID = 4, RoleID = 1 },
                new UserRole { UserID = 3, RoleID = 1 },
                new UserRole { UserID = 2, RoleID = 1 },
                new UserRole { UserID = 1, RoleID = 1 }
            );
            context.SaveChanges();

            context.Tables.AddRange(
                new Table { TableName = "Tafel 1", CompanyName = "Peterselie Bedrijf", Location = "Molenstraat 21, 2000 Antwerpen (Lokaal 3)", ContactUserID = 5 },
                new Table { TableName = "Tafel 2", CompanyName = "Peterselie Bedrijf", Location = "Molenstraat 21, 2000 Antwerpen (Lokaal 5)", ContactUserID = 5 },
                new Table { TableName = "Tafel 3", CompanyName = "Tomaten Bedrijf", Location = "Arnold Van Loonlaan 48, 3500 Hasselt (Lokaal 1)", ContactUserID = 4 }
            );
            context.SaveChanges();

            context.MatchTypes.AddRange(
                new MatchType { TypeName="1v1" },
                new MatchType { TypeName = "2v2" }
            );
            context.SaveChanges();

            context.MatchStatuses.AddRange(
                new MatchStatus { StatusName = "Planned"},
                new MatchStatus { StatusName = "Playing" },
                new MatchStatus { StatusName = "Played" },
                new MatchStatus { StatusName = "Cancelled" },
                new MatchStatus { StatusName = "Request" },
                new MatchStatus { StatusName = "Accept" }
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

            context.TeamStatuses.AddRange(
                new TeamStatus { StatusName = "Closed" },
                new TeamStatus { StatusName = "Review" },
                new TeamStatus { StatusName = "Open" }
            );
            context.SaveChanges();

            context.Teams.AddRange(
                new Team { TeamName = "De peterselies", CaptainID = 5, Location = "Antwerpen", CompanyName = "Peterselie Bedrijf", TeamStatusID = 2, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/dc8fec0b-1df4-48a5-a511-0b32872c234bistock_peterselie.jpg" },
                new Team { TeamName = "De tomaten", CaptainID = 4, Location = "Hasselt", CompanyName = "Tomaten Bedrijf", TeamStatusID = 2, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/c570a63c-789a-48b9-a768-de7e3dba84d9tomaten_voedingswaarde.jpg" },
                new Team { TeamName = "Het appelsientje", CaptainID = 3, Location = "Gent", CompanyName = "Appelsientjes Bedrijf", TeamStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/69798a3a-8dd4-435f-95c4-d9ac4362ecd0Appelsien.jpg" },
                new Team { TeamName = "De bananentros", CaptainID = 2, Location = "Brugge", CompanyName = "Bananen Bedrijf", TeamStatusID = 3, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/536c77a1-9309-4401-add7-55d2e4b51ea3banaan_bananen_voedingswaardetabel.jpg" }
            );
            context.SaveChanges();

            context.UserTeamStatuses.AddRange(
                new UserTeamStatus { StatusName = "In Review" },
                new UserTeamStatus { StatusName = "Accepted" }
            );
            context.SaveChanges();

            context.UserTeam.AddRange(
                new UserTeam { TeamID = 4, UserID = 5, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 3, UserID = 4, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 2, UserID = 3, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 1, UserID = 2, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 4, UserID = 1, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 3, UserID = 1, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 2, UserID = 1, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 1, UserID = 1, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 4, UserID = 2, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 4, UserID = 3, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 3, UserID = 2, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 3, UserID = 3, UserTeamStatusID = 1 },
                new UserTeam { TeamID = 2, UserID = 2, UserTeamStatusID = 1 }
            );
            context.SaveChanges();

            context.Matches.AddRange(
                new Match { Date = new DateTime(2020, 12, 8), Score1 = 4, Score2 = 6, TableID = 1, MatchTypeID = 2, Player1ID = 5, Player3ID = 4, MatchStatusID = 4 },
                new Match { Date = new DateTime(2020, 12, 9), Score1 = 8, Score2 = 4, TableID = 1, MatchTypeID = 2, Player1ID = 5, Player3ID = 3, MatchStatusID = 4 },
                new Match { Date = new DateTime(2020, 12, 10), Score1 = 5, Score2 = 6, TableID = 2, MatchTypeID = 2, Player1ID = 5, Player3ID = 2, MatchStatusID = 3 },
                new Match { Date = new DateTime(2020, 12, 10), Score1 = 6, Score2 = 4, TableID = 3, MatchTypeID = 2, Player1ID = 5, Player3ID = 4, MatchStatusID = 4 },
                new Match { Date = new DateTime(2020, 12, 11), Score1 = 8, Score2 = 7, Team1ID = 4, Team2ID = 3, TableID = 1, MatchTypeID = 1, Player1ID = 5, Player2ID = 3, Player3ID = 4, Player4ID = 2, MatchStatusID = 4 },
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 0, Score2 = 0, Team1ID = 4, Team2ID = 3, TableID = 1, MatchTypeID = 1, Player1ID = 5, Player2ID = 3, Player3ID = 4, Player4ID = 2, MatchStatusID = 6 }
            );
            context.SaveChanges();
        }
    }
}
