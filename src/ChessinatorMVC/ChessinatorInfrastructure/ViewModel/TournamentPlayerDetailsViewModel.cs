using ChessinatorDomain.Model;

namespace ChessinatorInfrastructure.ViewModel
{
    public class TournamentPlayerDetailsViewModel
    {
        public Tournament Tournament { get; set; } = null!;
        public IEnumerable<PlayerTournament>? PlayerTournaments { get; set; }
    }
}
