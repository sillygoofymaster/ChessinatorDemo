using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class TimeControl
{
    public int Id { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Базовий час")]
    public int BaseMinutes { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Інкремента")]
    public int IncSeconds { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Тип")]
    public string Type { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
