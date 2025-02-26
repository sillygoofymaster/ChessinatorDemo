using System;
using System.Collections.Generic;

namespace ChessinatorDomain.Model;

public partial class TimeControl:Entity
{
    public int BaseMinutes { get; set; }

    public int IncSeconds { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
