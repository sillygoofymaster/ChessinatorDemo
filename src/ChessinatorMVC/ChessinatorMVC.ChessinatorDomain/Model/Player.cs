using System;
using System.Collections.Generic;

namespace ChessinatorDomain.Model;

public partial class Player:Entity
{

    public int CurrentElo { get; set; }

    public int PeakElo { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public double Winrate { get; set; }

    public int TotalGamesCount { get; set; }

    public DateTime Birthday { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<ChessMatch> ChessMatchBlackPlayers { get; set; } = new List<ChessMatch>();

    public virtual ICollection<ChessMatch> ChessMatchWhitePlayers { get; set; } = new List<ChessMatch>();

    public virtual ICollection<PlayerTournament> PlayerTournaments { get; set; } = new List<PlayerTournament>();
}
