using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorDomain.Model;

public partial class Organizer : Entity
{
    [RequiredWithMessageAttribute]
    [Display(Name = "Ім'я")]
    public string FirstName { get; set; } = null!;

    [RequiredWithMessageAttribute]
    [Display(Name = "Прізвище")]
    public string LastName { get; set; } = null!;

    [RequiredWithMessageAttribute]
    [Display(Name = "Пошта")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    public string? Detais { get; set; }
    public string? Organization { get; set; } = null!;
    public virtual ICollection<Tournament> Tournaments { get; set; } = new List<Tournament>();

    public string UserId { get; set; } = null!;
    public User? User { get; set; }
    public string? ProfilePicturePath { get; set; }
}
 