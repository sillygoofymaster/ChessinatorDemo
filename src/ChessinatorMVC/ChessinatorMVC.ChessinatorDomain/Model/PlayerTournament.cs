using System;
using System.Collections.Generic;

namespace ChessinatorDomain.Model;

public partial class PlayerTournament : Entity
{
    public int? PlayerId { get; set; }

    public int TournamentId { get; set; }

    public int Score { get; set; }

    public virtual Player? Player { get; set; } = null!;

    public virtual Tournament Tournament { get; set; } = null!;
}
