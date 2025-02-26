using System;
using System.Collections.Generic;

namespace ChessinatorDomain.Model;

public partial class Organizer:Entity
{

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
