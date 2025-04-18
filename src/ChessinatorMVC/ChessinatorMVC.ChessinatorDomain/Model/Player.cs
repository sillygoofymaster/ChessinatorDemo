using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class Player : Entity
{

    [RequiredWithMessageAttribute]
    [Display(Name = "Титул")]
    public int TitleId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Поточний рейтинг")]
    public int CurrentElo { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Піковий рейтинг")]
    public int PeakElo { get; set; }

    [Display(Name ="Ім'я")]
    public string? FirstName { get; set; }

    [Display(Name = "Нікнейм")]
    public string Username { get; set; } = null!;

    [Display(Name = "Прізвище")]
    public string? LastName { get; set; }

    [Display(Name = "Зіграно матчів")]
    public int? TotalGamesCount { get; set; }

    [DataType(DataType.Date)]
    [RequiredWithMessageAttribute]
    [Display(Name = "Дата народження")]
    public DateTime Birthday { get; set; }

    [Display(Name = "Про себе (контактна інформація)")]
    public string? Details { get; set; }

    public virtual ICollection<ChessMatch> ChessMatchBlackPlayers { get; set; } = new List<ChessMatch>();

    public virtual ICollection<ChessMatch> ChessMatchWhitePlayers { get; set; } = new List<ChessMatch>();

    public virtual ICollection<PlayerTournament> PlayerTournaments { get; set; } = new List<PlayerTournament>();

    [Display(Name = "Титул")]
    public virtual Title? Title { get; set; }

    public string UserId { get; set; } = null!;
    public User? User { get; set; }
    public string? ProfilePicturePath { get; set; }
}
