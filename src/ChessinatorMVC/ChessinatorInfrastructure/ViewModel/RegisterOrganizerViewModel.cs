using ChessinatorDomain;
using System.ComponentModel.DataAnnotations;

namespace ChessinatorInfrastructure.ViewModel
{
    public class RegisterOrganizerViewModel:RegisterViewModel
    {
        [RequiredWithMessageAttribute]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; } = null!;

        [RequiredWithMessageAttribute]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; } = null!;

        [Required]
        [Display(Name = "Контактна пошта")]
        [DataType(DataType.EmailAddress)]
        public string OrgEmail { get; set; } = null!;

        [Display(Name = "Додаткові контакти (за бажанням)")]
        public string? Details { get; set; }

        [Display(Name = "Організація")]
        public string? Organization { get; set; } = null!;
    }
}
