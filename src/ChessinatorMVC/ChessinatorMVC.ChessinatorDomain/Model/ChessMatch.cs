using System;
using System.Collections.Generic;

namespace ChessinatorDomain.Model;

public partial class ChessMatch:Entity
{

    public int WhitePlayerId { get; set; }

    public int BlackPlayerId { get; set; }

    public int RoundNumber { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int Result { get; set; }

    public string Moves { get; set; } = null!;

    public int? TournamentId { get; set; }

    public virtual Player BlackPlayer { get; set; } = null!;

    public virtual Tournament? Tournament { get; set; }

    public virtual Player WhitePlayer { get; set; } = null!;
}
