using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class Title
{
    public int Id { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Стисла назва")]
    public string ShortName { get; set; } = null!;

    [Display(Name = "Повна назва")]
    public string? LongName { get; set; }

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
