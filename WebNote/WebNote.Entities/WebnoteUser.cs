using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNote.Entities
{
    public class WebnoteUser:EntityBase
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public Guid ActivateGuid { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Liked> Likes { get; set; }

    }
}
