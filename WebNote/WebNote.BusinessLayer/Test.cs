using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNote.Entities;
using WebNote.DataAccessLayer;
using WebNote.DataAccessLayer.EntityFramework;

namespace WebNote.BusinessLayer
{
    public class Test
    {
        private Repository<WebnoteUser> repo_user = new Repository<WebnoteUser>();
        public void test()
        {
            repo_user.Insert(new WebnoteUser
            {
                Username = "aa",
                Name = "aa",
                Email = "aa@aa.com",
                ActivateGuid = Guid.NewGuid(),
                Password = "123456",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                ModifiedUsername = "aa"


            }
            );
        }

    }
}
