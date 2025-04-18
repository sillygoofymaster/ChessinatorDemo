using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChessinatorDomain.Model;

public partial class ChessdbContext : IdentityDbContext<User>
{
    public ChessdbContext()
    {
    }

    public ChessdbContext(DbContextOptions<ChessdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChessMatch> ChessMatches { get; set; }

    public virtual DbSet<Organizer> Organizers { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerTournament> PlayerTournaments { get; set; }

    public virtual DbSet<TimeControl> TimeControls { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }
    public virtual DbSet<TimeControlType> TimeControlTypes { get; set; }
    public virtual DbSet<TournamentType> TournamentTypes { get; set; }
    public virtual DbSet<MatchResult> MatchResults { get; set; }
    public virtual DbSet<Round> Rounds { get; set; }
    public virtual DbSet<Opening> Openings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChessMatch>(entity =>
        {
            entity.ToTable("ChessMatch");

            entity.HasIndex(e => e.BlackPlayerId, "IX_ChessMatch_BlackPlayerId");

            entity.HasIndex(e => e.RoundId, "IX_ChessMatch_RoundId");

            entity.HasIndex(e => e.WhitePlayerId, "IX_ChessMatch_WhitePlayerId");

            entity.HasIndex(e => e.MatchResultId, "IX_ChessMatch_MatchResultId");

            entity.HasOne(d => d.BlackPlayer).WithMany(p => p.ChessMatchBlackPlayers)
                .HasForeignKey(d => d.BlackPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Round).WithMany(p => p.ChessMatches).HasForeignKey(d => d.RoundId).OnDelete(DeleteBehavior.SetNull);
            entity.HasOne(d => d.MatchResult).WithMany(p => p.Matches).HasForeignKey(d => d.MatchResultId).OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.WhitePlayer).WithMany(p => p.ChessMatchWhitePlayers)
                .HasForeignKey(d => d.WhitePlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.ToTable("Organizer");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Player");

            entity.HasIndex(e => e.TitleId, "IX_Player_TitleId");

            entity.Property(p => p.CurrentElo)
                    .HasDefaultValue(400);

            entity
                .Property(p => p.PeakElo)
                .HasDefaultValue(400); 


            entity.HasOne(d => d.Title).WithMany(p => p.Players).HasForeignKey(d => d.TitleId).OnDelete(DeleteBehavior.Restrict);

            entity.Property(e => e.TotalGamesCount).HasDefaultValue(0);

            entity.HasOne(p => p.User)
        .WithOne(u => u.PlayerProfile)
        .HasForeignKey<Player>(p => p.UserId) 
        .OnDelete(DeleteBehavior.Cascade); 

            entity
        .HasIndex(p => p.Username)
        .IsUnique();
        });

        modelBuilder.Entity<PlayerTournament>(entity =>
        {
            entity.ToTable("PlayerTournament");

            entity.HasIndex(e => e.PlayerId, "IX_PlayerTournament_PlayerId");

            entity.HasIndex(e => e.TournamentId, "IX_PlayerTournament_TournamentId");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerTournaments).HasForeignKey(d => d.PlayerId).OnDelete(DeleteBehavior.SetNull); 

            entity.HasOne(d => d.Tournament).WithMany(p => p.PlayerTournaments).HasForeignKey(d => d.TournamentId); 
        });

        modelBuilder.Entity<TimeControl>(entity =>
        {
            entity.ToTable("TimeControl");

            entity.HasIndex(e => e.TypeId, "IX_TimeControl_TypeId");
            entity.HasOne(e => e.Type).WithMany(p => p.TimeControls).HasForeignKey(e => e.TypeId).OnDelete(DeleteBehavior.Restrict);

        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.ToTable("Title");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.ToTable("Tournament");

            entity.HasIndex(e => e.OrganizerId, "IX_Tournament_OrganizerId");

            entity.HasIndex(e => e.TimeControlId, "IX_Tournament_TimeControlId");

            entity.HasIndex(e => e.VenueId, "IX_Tournament_VenueId");

            entity.HasIndex(e => e.TournamentTypeId, "IX_Tournament_TournamentTypeId");

            entity.Property(e => e.IsOpen).HasDefaultValue(true);

            entity.HasOne(d => d.Organizer).WithMany(p => p.Tournaments).HasForeignKey(d => d.OrganizerId).OnDelete(DeleteBehavior.SetNull); 

            entity.HasOne(d => d.TimeControl).WithMany(p => p.Tournaments).HasForeignKey(d => d.TimeControlId).OnDelete(DeleteBehavior.Restrict); 

            entity.HasOne(d => d.Venue).WithMany(p => p.Tournaments).HasForeignKey(d => d.VenueId).OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.TournamentType).WithMany(p => p.Tournaments).HasForeignKey(d => d.TournamentTypeId).OnDelete(DeleteBehavior.Restrict);

            entity
.HasIndex(p => p.Name)
.IsUnique();
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.ToTable("Venue");
        });

        modelBuilder.Entity<Organizer>()
        .HasOne(o => o.User) // Organizer has one User
        .WithOne(u => u.OrganizerProfile) // User has one Organizer
        .HasForeignKey<Organizer>(o => o.UserId) // Explicit FK
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(a => a.PlayerProfile)
                .WithOne(p=>p.User)
                .HasForeignKey<User>(a => a.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);

             entity.HasOne(a => a.OrganizerProfile)
                .WithOne(p=>p.User) 
                .HasForeignKey<User>(a => a.OrganizerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
