using System.Data.Entity;
using ORM.Models;

namespace ORM
{
    public class EntityModel : DbContext
    {
        static EntityModel()
        {
            Database.SetInitializer(new DbInitializer());
        }

        public EntityModel()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<ExceptionDetail> ExceptionDetails { get; set; }

    }
}