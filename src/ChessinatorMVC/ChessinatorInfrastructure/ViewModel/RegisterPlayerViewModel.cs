using ChessinatorDomain;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorInfrastructure.ViewModel
{
    public class RegisterPlayerViewModel: RegisterViewModel
    {
        [Display(Name = "Ім'я")]
        public string? FirstName { get; set; }

        [RequiredWithMessageAttribute]
        [Display(Name = "Нікнейм")]
        public string Username { get; set; } = null!;

        [Display(Name = "Прізвище")]
        public string? LastName { get; set; }

        [DataType(DataType.Date)]
        [RequiredWithMessageAttribute]
        [Display(Name = "Дата народження")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Про себе (контактна інформація)")]
        public string? Details { get; set; }
    }
}
