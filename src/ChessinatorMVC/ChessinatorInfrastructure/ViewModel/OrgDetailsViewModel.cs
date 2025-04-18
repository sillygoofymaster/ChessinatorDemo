using ChessinatorDomain.Model;

namespace ChessinatorInfrastructure.ViewModel
{
    public class OrgDetailsViewModel
    {
        public IFormFile? ProfilePicture { get; set; }
        public Organizer Organizer { get; set; } = null!;
        public IEnumerable<Tournament>? Tournaments { get; set; }
    }
}
