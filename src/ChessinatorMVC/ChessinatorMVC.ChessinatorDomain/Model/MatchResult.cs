using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class MatchResult
{
    public int Id { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Результат")]
    [StringLength(30, ErrorMessage = "Назва мусить містити від {1} до {2} символів.", MinimumLength = 3)]
    public string Result { get; set; } = null!;

    [Display(Name = "Опис")]
    public string? Description { get; set; }

    public virtual ICollection<ChessMatch> Matches { get; set; } = new List<ChessMatch>();
}