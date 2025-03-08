using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class ChessMatch
{
    public int Id { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Гравець за білих")]
    public int WhitePlayerId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Гравець за чорних")]
    public int BlackPlayerId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Номер раунду")]
    public int RoundNumber { get; set; }

    [DataType(DataType.Date)]
    [RequiredWithMessageAttribute]
    [Display(Name = "Дата початку")]
    public DateTime StartTime { get; set; }

    [DataType(DataType.Date)]
    [RequiredWithMessageAttribute]
    [Display(Name = "Дата кінця")]
    public DateTime EndTime { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Результат")]
    public int MatchResultId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Перебіг гри")]
    public string Moves { get; set; } = null!;

    [Display(Name = "Турнір")]
    public int? TournamentId { get; set; }

    [Display(Name = "Гравець за чорних")]
    public virtual Player? BlackPlayer { get; set; } = null!;

    [Display(Name = "Турнір")]
    public virtual Tournament? Tournament { get; set; }

    [Display(Name = "Гравець за білих")]
    public virtual Player? WhitePlayer { get; set; } = null!;

    public virtual MatchResult? MatchResult { get; set; }
}
