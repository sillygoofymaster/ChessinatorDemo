using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class Title : Entity
{

    [RequiredWithMessageAttribute]
    [Display(Name = "Стисла назва")]
    [StringLength(30, ErrorMessage = "Назва мусить містити від {1} до {2} символів.", MinimumLength = 2)]
    public string ShortName { get; set; } = null!;

    [Display(Name = "Повна назва")]
    [StringLength(30, ErrorMessage = "Назва мусить містити від {1} до {2} символів.", MinimumLength = 3)]
    public string? LongName { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
