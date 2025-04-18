﻿// <auto-generated />
using System;
using ChessinatorDomain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChessinatorInfrastructure.Migrations
{
    [DbContext(typeof(ChessdbContext))]
    [Migration("20250307102849_AddTDescription")]
    partial class AddTDescription
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChessinatorDomain.Model.ChessMatch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BlackPlayerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Moves")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("TournamentId")
                        .HasColumnType("int");

                    b.Property<int>("WhitePlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "BlackPlayerId" }, "IX_ChessMatch_BlackPlayerId");

                    b.HasIndex(new[] { "TournamentId" }, "IX_ChessMatch_TournamentId");

                    b.HasIndex(new[] { "WhitePlayerId" }, "IX_ChessMatch_WhitePlayerId");

                    b.ToTable("ChessMatch", (string)null);
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Organizer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizer", (string)null);
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<int>("CurrentElo")
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

                    b.Property<int>("Draws")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Loses")
                        .HasColumnType("int");

                    b.Property<int>("PeakElo")
                        .HasColumnType("int");

                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.Property<int?>("TotalGamesCount")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasComputedColumnSql("[Wins] + [Loses] + [Draws]");

                    b.Property<double?>("Winrate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("float")
                        .HasComputedColumnSql("CASE WHEN ([Wins] + [Loses] + [Draws]) = 0 THEN 0 ELSE CAST([Wins] AS FLOAT) / CAST([Wins] + [Loses] + [Draws] AS FLOAT) END");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TitleId" }, "IX_Player_TitleId");

                    b.ToTable("Player", (string)null);
                });

            modelBuilder.Entity("ChessinatorDomain.Model.PlayerTournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("TournamentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "PlayerId" }, "IX_PlayerTournament_PlayerId");

                    b.HasIndex(new[] { "TournamentId" }, "IX_PlayerTournament_TournamentId");

                    b.ToTable("PlayerTournament", (string)null);
                });

            modelBuilder.Entity("ChessinatorDomain.Model.TimeControl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BaseMinutes")
                        .HasColumnType("int");

                    b.Property<int>("IncSeconds")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("TimeControl", (string)null);
                });

            modelBuilder.Entity("ChessinatorDomain.Model.TimeControlType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("TimeControlTypes");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Title", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LongName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Title", (string)null);
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("OrganizerId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerLimit")
                        .HasColumnType("int");

                    b.Property<int>("RoundCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TimeControlId")
                        .HasColumnType("int");

                    b.Property<int>("TournamentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TournamentTypeId");

                    b.HasIndex(new[] { "OrganizerId" }, "IX_Tournament_OrganizerId");

                    b.HasIndex(new[] { "TimeControlId" }, "IX_Tournament_TimeControlId");

                    b.HasIndex(new[] { "VenueId" }, "IX_Tournament_VenueId");

                    b.ToTable("Tournament", (string)null);
                });

            modelBuilder.Entity("ChessinatorDomain.Model.TournamentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("TournamentTypes");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Venue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Venue", (string)null);
                });

            modelBuilder.Entity("ChessinatorDomain.Model.ChessMatch", b =>
                {
                    b.HasOne("ChessinatorDomain.Model.Player", "BlackPlayer")
                        .WithMany("ChessMatchBlackPlayers")
                        .HasForeignKey("BlackPlayerId")
                        .IsRequired();

                    b.HasOne("ChessinatorDomain.Model.Tournament", "Tournament")
                        .WithMany("ChessMatches")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("ChessinatorDomain.Model.Player", "WhitePlayer")
                        .WithMany("ChessMatchWhitePlayers")
                        .HasForeignKey("WhitePlayerId")
                        .IsRequired();

                    b.Navigation("BlackPlayer");

                    b.Navigation("Tournament");

                    b.Navigation("WhitePlayer");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Player", b =>
                {
                    b.HasOne("ChessinatorDomain.Model.Title", "Title")
                        .WithMany("Players")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Title");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.PlayerTournament", b =>
                {
                    b.HasOne("ChessinatorDomain.Model.Player", "Player")
                        .WithMany("PlayerTournaments")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChessinatorDomain.Model.Tournament", "Tournament")
                        .WithMany("PlayerTournaments")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.TimeControl", b =>
                {
                    b.HasOne("ChessinatorDomain.Model.TimeControlType", "Type")
                        .WithMany("TimeControls")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Tournament", b =>
                {
                    b.HasOne("ChessinatorDomain.Model.Organizer", "Organizer")
                        .WithMany("Tournaments")
                        .HasForeignKey("OrganizerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChessinatorDomain.Model.TimeControl", "TimeControl")
                        .WithMany("Tournaments")
                        .HasForeignKey("TimeControlId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChessinatorDomain.Model.TournamentType", "TournamentType")
                        .WithMany("Tournaments")
                        .HasForeignKey("TournamentTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ChessinatorDomain.Model.Venue", "Venue")
                        .WithMany("Tournaments")
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Organizer");

                    b.Navigation("TimeControl");

                    b.Navigation("TournamentType");

                    b.Navigation("Venue");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Organizer", b =>
                {
                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Player", b =>
                {
                    b.Navigation("ChessMatchBlackPlayers");

                    b.Navigation("ChessMatchWhitePlayers");

                    b.Navigation("PlayerTournaments");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.TimeControl", b =>
                {
                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.TimeControlType", b =>
                {
                    b.Navigation("TimeControls");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Title", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Tournament", b =>
                {
                    b.Navigation("ChessMatches");

                    b.Navigation("PlayerTournaments");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.TournamentType", b =>
                {
                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("ChessinatorDomain.Model.Venue", b =>
                {
                    b.Navigation("Tournaments");
                });
#pragma warning restore 612, 618
        }
    }
}
