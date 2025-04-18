using ChessinatorDomain.Model;
namespace ChessinatorInfrastructure.ViewModel
{
    public class TournamentDetailsViewModel
    {
        public Tournament Tournament { get; set; } = null!;
        public ChessMatch ChessMatch = new ChessMatch();
        public IEnumerable<ChessMatch>? ChessMatches { get; set; }
    }
}
