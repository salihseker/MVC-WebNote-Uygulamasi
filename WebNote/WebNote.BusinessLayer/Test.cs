using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNote.Entities;
using WebNote.DataAccessLayer;

namespace WebNote.BusinessLayer
{
    public class Test
    {

        public void test()
        {
            DatabaseContext db = new DatabaseContext();
            db.Database.CreateIfNotExists();
        }
        
    }
}
