using ClosedXML.Excel;
using ChessinatorDomain.Model;

namespace ChessinatorInfrastructure.Services;

public class CategoryImportService : IImportService<Tournament>
{
    private readonly ChessdbContext _context;
    
    public CategoryImportService(ChessdbContext context)
    {
        _context = context;
    }

    public async Task ImportFromStreamAsync(Stream stream, CancellationToken cancellationToken)
    {
        if (!stream.CanRead)
        {
            throw new ArgumentException("Дані не можуть бути прочитані", nameof(stream));
        }

        using (XLWorkbook workBook = new XLWorkbook(stream))
        {
            var tournamentSheet = workBook.Worksheet("Tournaments");
            var tournamentDictionary = new Dictionary<string, Tournament>();

            foreach (var row in tournamentSheet.RowsUsed().Skip(1))
            {
                string tournamentName = row.Cell("A").GetString();


                var tournament = new Tournament
                {
                    Name = tournamentName,
                    TournamentTypeId = row.Cell("B").GetValue<int>(),
                    StartTime = row.Cell("C").GetDateTime(),
                    EndTime = row.Cell("D").GetDateTime(),
                    IsOnline = row.Cell("E").GetBoolean(),
                    IsOpen = false,
                    PlayerLimit = row.Cell("F").GetValue<int>(),
                    RoundCount = row.Cell("G").GetValue<int>(),
                    Link = row.Cell("H").GetString(),
                    VenueId = row.Cell("I").GetValue<int?>(),
                    TimeControlId = row.Cell("J").GetValue<int>(),
                    OrganizerId = row.Cell("K").GetValue<int?>(),
                    TournamentPicturePath = row.Cell("M").GetString()
                };

                // Add tournament to context and our dictionary for lookup.
                _context.Tournaments.Add(tournament);
                tournamentDictionary[tournamentName] = tournament;
            }

            // Process Rounds sheet next.
            var roundsSheet = workBook.Worksheet("Rounds");

            // Store a dictionary to map rounds with a key that lets us attach matches.
            // For example, combine tournament identifier and round number.
            var roundDictionary = new Dictionary<(string tournamentKey, int roundNumber), Round>();

            foreach (var row in roundsSheet.RowsUsed().Skip(1))
            {
                // Assuming the first column holds the tournament identifier (e.g., Name)
                string tournamentKey = row.Cell("A").GetString();
                int roundNumber = row.Cell("B").GetValue<int>();

                // Look up the related tournament created earlier
                if (!tournamentDictionary.TryGetValue(tournamentKey, out var tournament))
                {
                    // Optionally handle cases where the tournament isn’t found.
                    continue;
                }

                var round = new Round
                {
                    RoundNumber = roundNumber,
                    StartTime = row.Cell("C").GetDateTime(),
                    EndTime = row.Cell("D").GetDateTime(),
                    TournamentId = tournament.Id,  // assuming Id is generated on insert; if not, adjust accordingly.
                    Tournament = tournament
                };

                // You can add the round to the tournament’s collection if your design requires it.
                tournament.Rounds.Add(round);
                roundDictionary[(tournamentKey, roundNumber)] = round;

                // Also add the round to the context if needed.
                _context.Rounds.Add(round);
            }

            // Process Matches sheet.
            var matchesSheet = workBook.Worksheet("Matches");
            foreach (var row in matchesSheet.RowsUsed().Skip(1))
            {
                // Assuming your matches sheet contains tournament identifier + round number
                // so we can locate which round this match belongs to.
                string tournamentKey = row.Cell("A").GetString(); // Tournament identifier column
                int roundNumber = row.Cell("B").GetValue<int>();

                // Look up the round
                if (!roundDictionary.TryGetValue((tournamentKey, roundNumber), out var round))
                {
                    // Optionally log or handle an error if round is not found.
                    continue;
                }

                // Create and populate the ChessMatch instance.
                var match = new ChessMatch
                {
/*                    WhitePlayerId = row.Cell("C").GetValue<int?>(),
                    BlackPlayerId = row.Cell("D").GetValue<int?>(),
                    MatchResultId = row.Cell("E").GetValue<int>(),
                    Moves = row.Cell("F").GetString(),
                    IsConcluded = row.Cell("G").GetValue<int>(),
                    ConcludedByBlack = row.Cell("H").GetString(),
                    ConcludedByWhite = row.Cell("I").GetString(),
                    RoundId = round.Id,  // Set the foreign key; adjust if Id is auto-generated.
                    Round = round*/
                };

                // Add match to round collection for navigation property, if desired.
                round.ChessMatches.Add(match);
                _context.ChessMatches.Add(match);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
