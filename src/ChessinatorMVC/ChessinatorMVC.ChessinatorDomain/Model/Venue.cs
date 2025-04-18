using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class Venue : Entity
{

    [RequiredWithMessageAttribute]
    [Display(Name = "Назва")]
    [StringLength(30, ErrorMessage = "Назва мусить містити від {1} до {2} символів.", MinimumLength = 3)]
    public string Name { get; set; } = null!;

    [RequiredWithMessageAttribute]
    [Display(Name = "Місто")]
    public string City { get; set; } = null!;

    [RequiredWithMessageAttribute]
    [Display(Name = "Країна")]
    public string Country { get; set; } = null!;

    [RequiredWithMessageAttribute]
    [Display(Name = "Адреса")]
    public string Adress { get; set; } = null!;

    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();
}
