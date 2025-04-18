using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessinatorDomain.Model
{
    public class User : IdentityUser
    {
        public int? PlayerId { get; set; }
        public virtual Player? PlayerProfile { get; set; }

        public int? OrganizerId { get; set; }
        public virtual Organizer? OrganizerProfile { get; set; }
    }
}

