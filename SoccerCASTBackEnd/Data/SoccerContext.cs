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
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchType> MatchTypes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<UserTeam> UserTeam { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
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

            modelBuilder.Entity<RolePermission>()
                .ToTable("RolePermissions");

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Permission>()
                .ToTable("Permissions");

            modelBuilder.Entity<Team>()
                .ToTable("Teams");

            modelBuilder.Entity<Match>()
                .ToTable("Matches");

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

            modelBuilder.Entity<Competition>()
                .ToTable("Competitions");

            modelBuilder.Entity<Tournament>()
                .ToTable("Tournaments");

            modelBuilder.Entity<MatchStatus>()
                .ToTable("MatchStatuses");
        }
    }
}
