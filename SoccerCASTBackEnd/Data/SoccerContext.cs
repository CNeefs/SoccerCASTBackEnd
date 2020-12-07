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
        public DbSet<Role> Roles { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchType> MatchTypes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<MatchStatus> MatchStatuses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<Match>().ToTable("Matches");
            modelBuilder.Entity<MatchType>().ToTable("MatchTypes");
            modelBuilder.Entity<Table>().ToTable("Tables");
            modelBuilder.Entity<UserTeam>().ToTable("UserTeam");
            modelBuilder.Entity<Competition>().ToTable("Competitions");
            modelBuilder.Entity<Tournament>().ToTable("Tournaments");
            modelBuilder.Entity<MatchStatus>().ToTable("MatchStatuses");
        }
    }
}
