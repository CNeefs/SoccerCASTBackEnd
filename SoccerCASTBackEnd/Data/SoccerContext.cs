using Microsoft.EntityFrameworkCore;
using SoccerCASTBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerCASTBackEnd.Data {
    public class SoccerContext : DbContext {
        public SoccerContext(DbContextOptions<SoccerContext> options) 
            : base(options) 
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; } 
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<TeamStatus> TeamStatuses { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchType> MatchTypes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }
        public DbSet<UserTeamStatus> UserTeamStatuses { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentTeam> TournamentTeams { get; set; }
        public DbSet<MatchStatus> MatchStatuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<UserRole>()
                .ToTable("UserRoles");

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                .ToTable("Roles");

            modelBuilder.Entity<Permission>()
                .ToTable("Permissions");

            modelBuilder.Entity<TeamStatus>()
                .ToTable("TeamStatuses");

            modelBuilder.Entity<Team>()
                .ToTable("Teams");

            modelBuilder.Entity<Match>()
                .ToTable("Matches");

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Competition)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Tournament)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.MatchType)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.MatchStatus)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Table)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchType>()
                .ToTable("MatchTypes");

            modelBuilder.Entity<Table>()
                .ToTable("Tables");

            modelBuilder.Entity<UserTeam>()
                .ToTable("UserTeam");

            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTeam>()
                .HasOne(ut => ut.Team)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTeamStatus>()
               .ToTable("UserTeamStatuses");

            modelBuilder.Entity<Competition>()
                .ToTable("Competitions");

            modelBuilder.Entity<Tournament>()
                .ToTable("Tournaments");

            modelBuilder.Entity<TournamentTeam>()
                .ToTable("TournamentTeams");

            modelBuilder.Entity<MatchStatus>()
                .ToTable("MatchStatuses");
        }
    }
}
