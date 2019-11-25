namespace tlogNew.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Model1 : DbContext
    {

        public Model1()
            : base("name=Model12")
        {
        }
        public DbSet<User> user { get; set; }
        public DbSet<Microblog> microblog { get; set; }
        public DbSet<Megablog> megablog { get; set; }
        public DbSet<Tag> tag { get; set; }
        public DbSet<Category> category { get; set; }

    }
}