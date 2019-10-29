using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebNote.Entities;

namespace WebNote.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DbSet<WebnoteUser> WebnoteUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }
    }
}
