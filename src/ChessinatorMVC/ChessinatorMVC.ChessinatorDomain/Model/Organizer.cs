using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class Organizer
{
    public int Id { get; set; }

    [RequiredWithMessageAttribute]
    [Display(Name = "Ім'я")]
    public string Name { get; set; } = null!;

    [RequiredWithMessageAttribute]
    [Display(Name = "Пошта")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
