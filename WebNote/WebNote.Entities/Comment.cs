using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNote.Entities
{
    [Table("Comments")]
    public class Comment : EntityBase
    {
        [Required,StringLength(300)]
        public string Text { get; set; }

        public virtual Note Note { get; set; }
        public virtual WebnoteUser Owner { get; set; }
    }
}
