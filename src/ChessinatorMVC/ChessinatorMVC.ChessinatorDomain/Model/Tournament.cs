using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class Tournament
{
    public int Id { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Назва")]
    [StringLength(30, ErrorMessage = "Назва мусить містити від {1} до {2} символів.", MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [RequiredWithMessageAttribute]
    [Display(Name = "Тип")]
    public int TournamentTypeId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Дата початку")]
    public DateTime StartTime { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Дата завершення")]
    public DateTime EndTime { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Онлайн")]
    public bool IsOnline { get; set; }

    public bool IsOpen { get; set; } // generate equal to true

    [RequiredWithMessageAttribute]
    [Display(Name = "Максимальна кількість гравців")]
    public int PlayerLimit { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Кількість раундів")]
    public int RoundCount { get; set; }

    [Display(Name = "Посилання")]
    public string? Link { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Локація")]
    public int? VenueId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Часовий контроль")]
    public int TimeControlId { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Організатор")]
    public int OrganizerId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<ChessMatch> ChessMatches { get; set; } = new List<ChessMatch>();

    [Display(Name = "Організатор")]
    public virtual Organizer? Organizer { get; set; } = null!;

    public virtual ICollection<PlayerTournament> PlayerTournaments { get; set; } = new List<PlayerTournament>();

    [Display(Name = "Часовий контроль")]
    public virtual TimeControl? TimeControl { get; set; }

    [Display(Name = "Тип")]
    public virtual TournamentType? TournamentType { get; set; }

    [Display(Name = "Локація")]
    public virtual Venue? Venue { get; set; }
}
