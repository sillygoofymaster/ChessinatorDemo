using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class ChessMatch: Entity
{
    [Display(Name = "Гравець за білих")]
    public int? WhitePlayerId { get; set; }


    [Display(Name = "Гравець за чорних")]
    public int? BlackPlayerId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Результат")]
    public int MatchResultId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Перебіг гри")]
    public string Moves { get; set; } = null!;

    [Display(Name = "Гравець за чорних")]
    public virtual Player? BlackPlayer { get; set; }


    [Display(Name = "Гравець за білих")]
    public virtual Player? WhitePlayer { get; set; }

    public int? WhitePlayerElo { get; set; }
    public int? BlackPlayerElo { get; set; }



    [Display(Name ="Раунд")]
    public int? RoundId { get; set; }

    [Display(Name = "Раунд")]
    public virtual Round? Round { get; set; }
    public string? Opening { get; set; }
    public int? BlackElo { get; set; }
    public int? WhiteElo { get; set; }

    public virtual MatchResult? MatchResult { get; set; }
}
