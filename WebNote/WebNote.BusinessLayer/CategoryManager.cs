using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNote.Entities;
using WebNote.DataAccessLayer.EntityFramework;
using System.Linq.Expressions;

namespace WebNote.BusinessLayer
{
    public class CategoryManager
    {
        private Repository<Category> repo_category = new Repository<Category>();

        public List<Category> GetCategories()
        {
            return repo_category.List();
        }

        public Category Find(Expression<Func<Category, bool>> where)
        {
            return repo_category.Find(where);
        }
        
    }
}
