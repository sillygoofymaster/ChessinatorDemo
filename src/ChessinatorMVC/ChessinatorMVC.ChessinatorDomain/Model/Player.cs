using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class Player
{
    public int Id { get; set; }

    [Display(Name = "Ім'я")]
    public string? DisplayName { get; private set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Титул")]
    public int TitleId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Поточний рейтинг")]
    public int CurrentElo { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Піковий рейтинг")]
    public int PeakElo { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name ="Ім'я")]
    public string FirstName { get; set; } = null!;

    [RequiredWithMessageAttribute]
    [Display(Name = "Прізвище")]
    public string LastName { get; set; } = null!;

    public double? Winrate { get; private set; }

    [Display(Name = "Зіграно матчів")]
    public int? TotalGamesCount { get; private set; }

    [DataType(DataType.Date)]
    [RequiredWithMessageAttribute]
    [Display(Name = "Дата народження")]
    public DateTime Birthday { get; set; }

    [Display(Name = "Пошта")]
    public string? Email { get; set; }

    [Display(Name = "Кількість перемог")]
    public int Wins { get; set; }

    [Display(Name = "Кількість нічиїх")]
    public int Draws { get; set; }

    [Display(Name = "Кількість поразок")]
    public int Loses { get; set; }

    public virtual ICollection<ChessMatch> ChessMatchBlackPlayers { get; set; } = new List<ChessMatch>();

    public virtual ICollection<ChessMatch> ChessMatchWhitePlayers { get; set; } = new List<ChessMatch>();

    public virtual ICollection<PlayerTournament> PlayerTournaments { get; set; } = new List<PlayerTournament>();

    public virtual Title? Title { get; set; } = null!;
}
