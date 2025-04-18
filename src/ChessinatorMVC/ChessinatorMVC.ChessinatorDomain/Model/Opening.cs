using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessinatorDomain.Model
{
    public partial class Opening: Entity
    {
        public string Code { get; set; } = null!;
        public string Moves { get; set; } = null!;
        public string Title { get; set; } = null!;  
    }
}
