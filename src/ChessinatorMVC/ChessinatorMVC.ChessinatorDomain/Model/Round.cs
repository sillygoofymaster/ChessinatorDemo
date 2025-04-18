using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessinatorDomain.Model;

public partial class Round : Entity
{
    [RequiredWithMessageAttribute]
    [Display(Name = "#")]
    public int RoundNumber { get; set; }

    [DataType(DataType.Date)]
    [RequiredWithMessageAttribute]
    [Display(Name = "Дата початку")]
    public DateTime StartTime { get; set; }

    [DataType(DataType.Date)]
    [RequiredWithMessageAttribute]
    [Display(Name = "Дата кінця")]
    public DateTime EndTime { get; set; }

    [RequiredWithMessage]
    [Display(Name = "Турнір")]
    public int TournamentId { get; set; }

    [Display(Name = "Турнір")]
    public virtual Tournament? Tournament { get; set; }

    public virtual ICollection<ChessMatch> ChessMatches { get; set; } = new List<ChessMatch>();
}