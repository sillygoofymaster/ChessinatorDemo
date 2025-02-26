using System;
using System.Collections.Generic;

namespace ChessinatorDomain.Model;

public partial class Venue:Entity
{

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
