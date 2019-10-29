using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNote.Entities
{
    public class Category : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Note> Notes { get; set; }

    }
}
