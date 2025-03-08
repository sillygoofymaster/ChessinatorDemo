using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class TournamentType
{
    public int Id { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Назва")]
    [StringLength(30, ErrorMessage = "Назва мусить містити від {1} до {2} символів.", MinimumLength = 3)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();

}