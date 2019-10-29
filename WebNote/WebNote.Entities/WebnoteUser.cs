using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebNote.Entities
{
    [Table("WebnoteUsers")]
    public class WebnoteUser:EntityBase
    {
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(30)]
        public string Surname { get; set; }
        [Required,StringLength(30)]
        public string Username { get; set; }
        [Required, StringLength(100)]
        public string Email { get; set; }
        [Required, StringLength(500)]
        public string Password { get; set; }
        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public Guid ActivateGuid { get; set; }
        public bool IsAdmin { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Liked> Likes { get; set; }

    }
}
