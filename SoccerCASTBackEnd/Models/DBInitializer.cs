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

            Role admin = new Role { Name = "Admin" };
            Role user = new Role { Name = "User" };

            context.Roles.AddRange(user, admin);
            context.SaveChanges();

            context.Permissions.AddRange(
                new Permission { Name = "TABLE_MANAGE", RoleID = admin.RoleID },
                new Permission { Name = "TABLE_CREATE", RoleID = admin.RoleID },
                new Permission { Name = "TABLE_EDIT", RoleID = admin.RoleID },
                new Permission { Name = "TABLE_VIEW", RoleID = admin.RoleID },
                new Permission { Name = "COMPETITION_MANAGE", RoleID = admin.RoleID },
                new Permission { Name = "COMPETITION_CREATE", RoleID = admin.RoleID },
                new Permission { Name = "COMPETITION_EDIT", RoleID = admin.RoleID },
                new Permission { Name = "COMPETITION_VIEW", RoleID = admin.RoleID },
                new Permission { Name = "USER_MANAGE", RoleID = admin.RoleID },
                new Permission { Name = "USER_CREATE", RoleID = admin.RoleID },
                new Permission { Name = "USER_EDIT", RoleID = admin.RoleID },
                new Permission { Name = "USER_VIEW", RoleID = admin.RoleID },
                new Permission { Name = "TOURNAMENT_MANAGE", RoleID = admin.RoleID },
                new Permission { Name = "TOURNAMENT_CREATE", RoleID = admin.RoleID },
                new Permission { Name = "TOURNAMENT_EDIT", RoleID = admin.RoleID },
                new Permission { Name = "TOURNAMENT_VIEW", RoleID = admin.RoleID },
                new Permission { Name = "TOURNAMENT_JOIN", RoleID = admin.RoleID },
                new Permission { Name = "TOURNAMENT_JOIN", RoleID = user.RoleID },
                new Permission { Name = "TEAM_MANAGE", RoleID = admin.RoleID },
                new Permission { Name = "TEAM_CREATE", RoleID = admin.RoleID },
                new Permission { Name = "TEAM_CREATE", RoleID = user.RoleID },
                new Permission { Name = "TEAM_EDIT", RoleID = admin.RoleID },
                new Permission { Name = "TEAM_EDIT", RoleID = user.RoleID },
                new Permission { Name = "TEAM_ADD", RoleID = admin.RoleID },
                new Permission { Name = "TEAM_ADD", RoleID = user.RoleID },
                new Permission { Name = "TEAM_VIEW", RoleID = admin.RoleID },
                new Permission { Name = "TEAM_VIEW", RoleID = user.RoleID },
                new Permission { Name = "PROFILE_VIEW", RoleID = admin.RoleID },
                new Permission { Name = "PROFILE_VIEW", RoleID = user.RoleID },
                new Permission { Name = "PROFILE_EDIT", RoleID = admin.RoleID },
                new Permission { Name = "PROFILE_EDIT", RoleID = user.RoleID },
                new Permission { Name = "MATCH_START", RoleID = admin.RoleID },
                new Permission { Name = "MATCH_START", RoleID = user.RoleID },
                new Permission { Name = "MATCH_CHALLENGE", RoleID = admin.RoleID },
                new Permission { Name = "MATCH_CHALLENGE", RoleID = user.RoleID },
                new Permission { Name = "MATCH_ACCEPT", RoleID = admin.RoleID },
                new Permission { Name = "MATCH_ACCEPT", RoleID = user.RoleID },
                new Permission { Name = "RANKING_VIEW", RoleID = admin.RoleID },
                new Permission { Name = "RANKING_VIEW", RoleID = user.RoleID },
                new Permission { Name = "PROFILE_EDIT", RoleID = admin.RoleID },
                new Permission { Name = "PROFILE_EDIT", RoleID = user.RoleID },
                new Permission { Name = "PLAY_VIEW", RoleID = admin.RoleID },
                new Permission { Name = "PLAY_VIEW", RoleID = user.RoleID }
            );
            context.SaveChanges();

            context.UserStatuses.AddRange(
                new UserStatus { StatusName = "Disband" },
                new UserStatus { StatusName = "Exists" }
            );
            context.SaveChanges();

            User user1 = new User { Password = BCrypt.Net.BCrypt.HashPassword("adminfruitkom"), FirstName = "Peter", LastName = "Celie", Email = "player1@thomasmore.be", BirthDate = new DateTime(1969, 5, 4), TimesLost = 2, TimesWon = 4, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/9f686271-7aa3-415f-81c7-b7a44128b068Peterselie.jpg" };
            User user2 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Tom", LastName = "Ato", Email = "player2@thomasmore.be", BirthDate = new DateTime(1969, 11, 21), TimesLost = 3, TimesWon = 1, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/64a2f0fc-8b17-4651-ac96-0424403e739btomaat-groente-Veggipedia.png" };
            User user3 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Apple", LastName = "Sien", Email = "player3@thomasmore.be", BirthDate = new DateTime(1969, 3, 8), TimesLost = 2, TimesWon = 1, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/f14cdf2e-7e32-4836-9133-94ad9185a90976e3afed87dc07327310a312c2c65ffe.jpg" };
            User user4 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Ban", LastName = "Aan", Email = "player4@thomasmore.be", BirthDate = new DateTime(1969, 9, 28), TimesLost = 1, TimesWon = 3, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/c6c68100-7f89-423d-902b-e00d6877252bbanaan-vierkant.jpg" };
            User user5 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Ki", LastName = "Wi", Email = "player5@thomasmore.be", BirthDate = new DateTime(1969, 2, 4), TimesLost = 1, TimesWon = 0, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/1ebb586f-0afb-490e-ad1e-baa8cfba52e5Kiwi-fruits-582a07b.jpg" };
            User user6 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Black", LastName = "Berry", Email = "player6@thomasmore.be", BirthDate = new DateTime(1969, 3, 4), TimesLost = 1, TimesWon = 0, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user7 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Cher", LastName = "Ry", Email = "player7@thomasmore.be", BirthDate = new DateTime(1969, 4, 4), TimesLost = 1, TimesWon = 1, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user8 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Coco", LastName = "Nut", Email = "player8@thomasmore.be", BirthDate = new DateTime(1969, 5, 4), TimesLost = 1, TimesWon = 2, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user9 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Papa", LastName = "Ya", Email = "player9@thomasmore.be", BirthDate = new DateTime(1969, 6, 4), TimesLost = 1, TimesWon = 1, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user10 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Pine", LastName = "Apple", Email = "player10@thomasmore.be", BirthDate = new DateTime(1969, 7, 4), TimesLost = 2, TimesWon = 0, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user11 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Pump", LastName = "Kin", Email = "player11@thomasmore.be", BirthDate = new DateTime(1969, 8, 4), TimesLost = 1, TimesWon = 1, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user12 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Oli", LastName = "Ve", Email = "player12@thomasmore.be", BirthDate = new DateTime(1969, 9, 4), TimesLost = 0, TimesWon = 3, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user13 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Nect", LastName = "Arine", Email = "player13@thomasmore.be", BirthDate = new DateTime(1969, 10, 4), TimesLost = 1, TimesWon = 0, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user14 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Gra", LastName = "Pe", Email = "player14@thomasmore.be", BirthDate = new DateTime(1969, 11, 4), TimesLost = 1, TimesWon = 0, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user15 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Mel", LastName = "On", Email = "player15@thomasmore.be", BirthDate = new DateTime(1969, 12, 4), TimesLost = 1, TimesWon = 1, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };
            User user16 = new User { Password = BCrypt.Net.BCrypt.HashPassword("fruitkom"), FirstName = "Cac", LastName = "Ao", Email = "player16@thomasmore.be", BirthDate = new DateTime(1969, 5, 24), TimesLost = 1, TimesWon = 2, UserStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-profile-picture.webp" };


            context.Users.AddRange(user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, user11, user12, user13, user14, user15, user16);
            context.SaveChanges();

            context.UserRoles.AddRange(
                new UserRole { UserID = user1.UserID, RoleID = 2 },
                new UserRole { UserID = user1.UserID, RoleID = 1 },
                new UserRole { UserID = user2.UserID, RoleID = 1 },
                new UserRole { UserID = user3.UserID, RoleID = 1 },
                new UserRole { UserID = user4.UserID, RoleID = 1 },
                new UserRole { UserID = user5.UserID, RoleID = 1 },
                new UserRole { UserID = user6.UserID, RoleID = 1 },
                new UserRole { UserID = user7.UserID, RoleID = 1 },
                new UserRole { UserID = user8.UserID, RoleID = 1 },
                new UserRole { UserID = user9.UserID, RoleID = 1 },
                new UserRole { UserID = user10.UserID, RoleID = 1 },
                new UserRole { UserID = user11.UserID, RoleID = 1 },
                new UserRole { UserID = user12.UserID, RoleID = 1 },
                new UserRole { UserID = user13.UserID, RoleID = 1 },
                new UserRole { UserID = user14.UserID, RoleID = 1 },
                new UserRole { UserID = user15.UserID, RoleID = 1 },
                new UserRole { UserID = user16.UserID, RoleID = 1 }
            );
            context.SaveChanges();

            context.TableStatuses.AddRange(
                new TableStatus { StatusName = "Disband" },
                new TableStatus { StatusName = "Exists" }
            );
            context.SaveChanges();

            context.Tables.AddRange(
                new Table { TableName = "Tafel 1", CompanyName = "Peterselie Bedrijf", Location = "Molenstraat 21, 2000 Antwerpen (Lokaal 3)", ContactUserID = user1.UserID, TableStatusID = 1 },
                new Table { TableName = "Tafel 2", CompanyName = "Peterselie Bedrijf", Location = "Molenstraat 21, 2000 Antwerpen (Lokaal 5)", ContactUserID = user1.UserID, TableStatusID = 1 },
                new Table { TableName = "Tafel 3", CompanyName = "Tomaten Bedrijf", Location = "Arnold Van Loonlaan 48, 3500 Hasselt (Lokaal 1)", ContactUserID = user2.UserID, TableStatusID = 1 }
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

            context.TournamentStatuses.AddRange(
                new TournamentStatus { StatusName = "Disband" },
                new TournamentStatus { StatusName = "Exists" }
            );
            context.SaveChanges();

            Tournament tournament1 = new Tournament { Edition = "Doomsday 2020", Total_Joined = 8, Match_Count = 8, isStart = false, Winner = "De bananentros", TableID = 1, TournamentStatusID = 1 };
            Tournament tournament2 = new Tournament { Edition = "Fruit Bowl 2020", Total_Joined = 4, Match_Count = 8, isStart = false, TableID = 1,TournamentStatusID = 1 };

            context.Tournaments.AddRange(tournament1, tournament2);
            context.SaveChanges();

            context.Competitions.Add(
                new Competition { Name="Eerste klasse", isActive = true}
            );
            context.SaveChanges();

            context.TeamStatuses.AddRange(
                new TeamStatus { StatusName = "Disband" },
                new TeamStatus { StatusName = "Closed" },
                new TeamStatus { StatusName = "Review" },
                new TeamStatus { StatusName = "Open" }
            );
            context.SaveChanges();

            Team team1 = new Team { TeamName = "De Peterselies", CaptainID = user1.UserID, Location = "Antwerpen", CompanyName = "Peterselie Bedrijf", TeamStatusID = 2, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/dc8fec0b-1df4-48a5-a511-0b32872c234bistock_peterselie.jpg" };
            Team team2 = new Team { TeamName = "De Tomaten", CaptainID = user2.UserID, Location = "Hasselt", CompanyName = "Tomaten Bedrijf", TeamStatusID = 2, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/c570a63c-789a-48b9-a768-de7e3dba84d9tomaten_voedingswaarde.jpg" };
            Team team3 = new Team { TeamName = "Het Appelsientje", CaptainID = user3.UserID, Location = "Gent", CompanyName = "Appelsientjes Bedrijf", TeamStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/69798a3a-8dd4-435f-95c4-d9ac4362ecd0Appelsien.jpg" };
            Team team4 = new Team { TeamName = "De Bananentros", CaptainID = user4.UserID, Location = "Brugge", CompanyName = "Bananen Bedrijf", TeamStatusID = 3, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/536c77a1-9309-4401-add7-55d2e4b51ea3banaan_bananen_voedingswaardetabel.jpg" };
            Team team5 = new Team { TeamName = "De Kiwilinies", CaptainID = user5.UserID, Location = "Brussel", CompanyName = "Kiwi Bedrijf", TeamStatusID = 2, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-team-picture.jpg" };
            Team team6 = new Team { TeamName = "Blackberry Mobile", CaptainID = user6.UserID, Location = "Oostende", CompanyName = "Blackberry Bedrijf", TeamStatusID = 2, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-team-picture.jpg" };
            Team team7 = new Team { TeamName = "Cherrryyyyy", CaptainID = user7.UserID, Location = "Lommel", CompanyName = "Cherry Bedrijf", TeamStatusID = 1, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-team-picture.jpg" };
            Team team8 = new Team { TeamName = "Cocoloco", CaptainID = user8.UserID, Location = "Leuven", CompanyName = "Coconut Bedrijf", TeamStatusID = 3, ImagePath = "https://soccercastpictures.blob.core.windows.net/firstcontainer/blank-team-picture.jpg" };



            context.Teams.AddRange(team1, team2, team3, team4, team5, team6, team7, team8);
            context.SaveChanges();

            context.UserTeamStatuses.AddRange(
                new UserTeamStatus { StatusName = "In Review" },
                new UserTeamStatus { StatusName = "Accepted" }
            );
            context.SaveChanges();

            context.UserTeam.AddRange(
                new UserTeam { TeamID = team1.TeamID, UserID = user1.UserID, UserTeamStatusID = 1},
                new UserTeam { TeamID = team1.TeamID, UserID = user9.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team2.TeamID, UserID = user2.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team2.TeamID, UserID = user10.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team3.TeamID, UserID = user3.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team3.TeamID, UserID = user11.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team4.TeamID, UserID = user4.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team4.TeamID, UserID = user12.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team5.TeamID, UserID = user5.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team5.TeamID, UserID = user13.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team6.TeamID, UserID = user6.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team6.TeamID, UserID = user14.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team7.TeamID, UserID = user7.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team7.TeamID, UserID = user15.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team8.TeamID, UserID = user8.UserID, UserTeamStatusID = 1 },
                new UserTeam { TeamID = team8.TeamID, UserID = user16.UserID, UserTeamStatusID = 1 }
            );
            context.SaveChanges();

            context.TournamentTeams.AddRange(
                new TournamentTeam { TournamentID = tournament1.TournamentID, TeamID = team1.TeamID, Player1ID = user1.UserID, Player2ID = user9.UserID },
                new TournamentTeam { TournamentID = tournament1.TournamentID, TeamID = team2.TeamID, Player1ID = user2.UserID, Player2ID = user10.UserID },
                new TournamentTeam { TournamentID = tournament1.TournamentID, TeamID = team3.TeamID, Player1ID = user3.UserID, Player2ID = user11.UserID },
                new TournamentTeam { TournamentID = tournament1.TournamentID, TeamID = team4.TeamID, Player1ID = user4.UserID, Player2ID = user12.UserID },
                new TournamentTeam { TournamentID = tournament1.TournamentID, TeamID = team5.TeamID, Player1ID = user5.UserID, Player2ID = user13.UserID },
                new TournamentTeam { TournamentID = tournament1.TournamentID, TeamID = team6.TeamID, Player1ID = user6.UserID, Player2ID = user14.UserID },
                new TournamentTeam { TournamentID = tournament1.TournamentID, TeamID = team7.TeamID, Player1ID = user7.UserID, Player2ID = user15.UserID },
                new TournamentTeam { TournamentID = tournament1.TournamentID, TeamID = team8.TeamID, Player1ID = user8.UserID, Player2ID = user16.UserID },
                new TournamentTeam { TournamentID = tournament2.TournamentID, TeamID = team1.TeamID, Player1ID = user1.UserID, Player2ID = user9.UserID },
                new TournamentTeam { TournamentID = tournament2.TournamentID, TeamID = team2.TeamID, Player1ID = user2.UserID, Player2ID = user10.UserID },
                new TournamentTeam { TournamentID = tournament2.TournamentID, TeamID = team3.TeamID, Player1ID = user3.UserID, Player2ID = user11.UserID },
                new TournamentTeam { TournamentID = tournament2.TournamentID, TeamID = team4.TeamID, Player1ID = user4.UserID, Player2ID = user12.UserID }
            );
            context.SaveChanges();

            context.Matches.AddRange(
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 4, Score2 = 5, TableID = 1, MatchTypeID = 1, Team1ID = team1.TeamID, Team2ID = team8.TeamID, Player1ID = user1.UserID, Player2ID = user9.UserID, Player3ID = user8.UserID, Player4ID = user16.UserID, MatchStatusID = 4, TournamentID = tournament1.TournamentID, Round = 1, Number = 1, NextRound = 1 },
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 5, Score2 = 9, TableID = 1, MatchTypeID = 1, Team1ID = team2.TeamID, Team2ID = team7.TeamID, Player1ID = user2.UserID, Player2ID = user10.UserID, Player3ID = user7.UserID, Player4ID = user15.UserID, MatchStatusID = 4, TournamentID = tournament1.TournamentID, Round = 1, Number = 2, NextRound = 1 },
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 8, Score2 = 7, TableID = 1, MatchTypeID = 1, Team1ID = team3.TeamID, Team2ID = team6.TeamID, Player1ID = user3.UserID, Player2ID = user11.UserID, Player3ID = user6.UserID, Player4ID = user14.UserID, MatchStatusID = 4, TournamentID = tournament1.TournamentID, Round = 1, Number = 3, NextRound = 2 },
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 6, Score2 = 4, TableID = 1, MatchTypeID = 1, Team1ID = team4.TeamID, Team2ID = team5.TeamID, Player1ID = user4.UserID, Player2ID = user12.UserID, Player3ID = user5.UserID, Player4ID = user13.UserID, MatchStatusID = 4, TournamentID = tournament1.TournamentID, Round = 1, Number = 4, NextRound = 2 },
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 8, Score2 = 6, TableID = 1, MatchTypeID = 1, Team1ID = team8.TeamID, Team2ID = team7.TeamID, Player1ID = user8.UserID, Player2ID = user16.UserID, Player3ID = user7.UserID, Player4ID = user15.UserID, MatchStatusID = 4, TournamentID = tournament1.TournamentID, Round = 2, Number = 1, NextRound = 1 },
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 6, Score2 = 7, TableID = 1, MatchTypeID = 1, Team1ID = team3.TeamID, Team2ID = team4.TeamID, Player1ID = user3.UserID, Player2ID = user11.UserID, Player3ID = user4.UserID, Player4ID = user12.UserID, MatchStatusID = 4, TournamentID = tournament1.TournamentID, Round = 2, Number = 2, NextRound = 1 },
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 7, Score2 = 8, TableID = 1, MatchTypeID = 1, Team1ID = team8.TeamID, Team2ID = team4.TeamID, Player1ID = user8.UserID, Player2ID = user16.UserID, Player3ID = user4.UserID, Player4ID = user12.UserID, MatchStatusID = 4, TournamentID = tournament1.TournamentID, Round = 3, Number = 1, NextRound = 1 },
                new Match { Date = new DateTime(2020, 12, 8), Score1 = 4, Score2 = 6, TableID = 1, MatchTypeID = 2, Player1ID = user1.UserID, Player3ID = user2.UserID, MatchStatusID = 4 },
                new Match { Date = new DateTime(2020, 12, 9), Score1 = 8, Score2 = 4, TableID = 1, MatchTypeID = 2, Player1ID = user1.UserID, Player3ID = user3.UserID, MatchStatusID = 4 },
                new Match { Date = new DateTime(2020, 12, 10), Score1 = 5, Score2 = 6, TableID = 2, MatchTypeID = 2, Player1ID = user1.UserID, Player3ID = user4.UserID, MatchStatusID = 3 },
                new Match { Date = new DateTime(2020, 12, 10), Score1 = 6, Score2 = 4, TableID = 3, MatchTypeID = 2, Player1ID = user1.UserID, Player3ID = user2.UserID, MatchStatusID = 4 },
                new Match { Date = new DateTime(2020, 12, 11), Score1 = 8, Score2 = 7, Team1ID = team1.TeamID, Team2ID = team2.TeamID, TableID = 1, MatchTypeID = 1, Player1ID = user1.UserID, Player2ID = user9.UserID, Player3ID = user2.UserID, Player4ID = user10.UserID, MatchStatusID = 4 },
                new Match { Date = new DateTime(2020, 12, 12), Score1 = 0, Score2 = 0, Team1ID = team1.TeamID, Team2ID = team2.TeamID, TableID = 1, MatchTypeID = 1, Player1ID = user1.UserID, Player2ID = user9.UserID, Player3ID = user2.UserID, Player4ID = user10.UserID, MatchStatusID = 6 }
            );
            context.SaveChanges();
        }
    }
}
