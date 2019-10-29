using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebNote.Entities
{
    public class Comment : EntityBase
    {
        public string Text { get; set; }

        public virtual Note Note { get; set; }
        public virtual WebnoteUser Owner { get; set; }
    }
}
