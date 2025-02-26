using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ChessinatorDomain.Model;

namespace ChessinatorInfrastructure;

public partial class ChessdbContext : DbContext
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

    public virtual DbSet<Tournament> Tournaments { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-4USRHJP\\SQLEXPRESS; Database=Chessdb; Trusted_Connection=True; TrustServerCertificate=True; ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChessMatch>(entity =>
        {
            entity.ToTable("ChessMatch");

            entity.HasIndex(e => e.BlackPlayerId, "IX_ChessMatch_BlackPlayerId");

            entity.HasIndex(e => e.TournamentId, "IX_ChessMatch_TournamentId");

            entity.HasIndex(e => e.WhitePlayerId, "IX_ChessMatch_WhitePlayerId");

            entity.HasOne(d => d.BlackPlayer).WithMany(p => p.ChessMatchBlackPlayers)
                .HasForeignKey(d => d.BlackPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Tournament).WithMany(p => p.ChessMatches).HasForeignKey(d => d.TournamentId);

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
        });

        modelBuilder.Entity<PlayerTournament>(entity =>
        {
            entity.ToTable("PlayerTournament");

            entity.HasIndex(e => e.PlayerId, "IX_PlayerTournament_PlayerId");

            entity.HasIndex(e => e.TournamentId, "IX_PlayerTournament_TournamentId");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerTournaments).HasForeignKey(d => d.PlayerId);

            entity.HasOne(d => d.Tournament).WithMany(p => p.PlayerTournaments).HasForeignKey(d => d.TournamentId);
        });

        modelBuilder.Entity<TimeControl>(entity =>
        {
            entity.ToTable("TimeControl");
        });

        modelBuilder.Entity<Tournament>(entity =>
        {
            entity.ToTable("Tournament");

            entity.HasIndex(e => e.OrganizerId, "IX_Tournament_OrganizerId");

            entity.HasIndex(e => e.TimeControlId, "IX_Tournament_TimeControlId");

            entity.HasIndex(e => e.VenueId, "IX_Tournament_VenueId");

            entity.HasOne(d => d.Organizer).WithMany(p => p.Tournaments).HasForeignKey(d => d.OrganizerId);

            entity.HasOne(d => d.TimeControl).WithMany(p => p.Tournaments).HasForeignKey(d => d.TimeControlId);

            entity.HasOne(d => d.Venue).WithMany(p => p.Tournaments).HasForeignKey(d => d.VenueId);
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.ToTable("Venue");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
