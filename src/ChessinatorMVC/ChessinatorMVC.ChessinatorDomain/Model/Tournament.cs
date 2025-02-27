using System;
using System.Collections.Generic;

namespace ChessinatorDomain.Model;

public partial class Tournament
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public bool IsOnline { get; set; }

    public bool IsOpen { get; set; }

    public int PlayerLimit { get; set; }

    public int RoundCount { get; set; }

    public string? Link { get; set; }

    public int? VenueId { get; set; }

    public int TimeControlId { get; set; }

    public int OrganizerId { get; set; }

    public virtual ICollection<ChessMatch> ChessMatches { get; set; } = new List<ChessMatch>();

    public virtual Organizer Organizer { get; set; } = null!;

    public virtual ICollection<PlayerTournament> PlayerTournaments { get; set; } = new List<PlayerTournament>();

    public virtual TimeControl TimeControl { get; set; } = null!;

    public virtual Venue? Venue { get; set; }
}
