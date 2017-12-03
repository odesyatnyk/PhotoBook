using System.Data.Entity;
using System.Web.Helpers;
using ORM.Models;

namespace ORM
{
    public class DbInitializer : CreateDatabaseIfNotExists<EntityModel> //Different modes (4 types)
    {
        protected override void Seed(EntityModel context)
        {
            #region Roles initializing
            var role = new Role
            {
                Name = "User"
            };
            var adminRole = new Role
            {
                Name = "Admin"
            };
            var moderRole = new Role()
            {
                Name = "Moderator"
            };
            context.Roles.Add(role);
            context.Roles.Add(moderRole);
            context.Roles.Add(adminRole);
            context.SaveChanges();
            #endregion

            #region Users initializing
            var user1 = new User
            {
                Email = "arya.stark@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
                Role = role,
                RoleId = role.RoleId,
                UserName = "AryaWolf"
            };
            var user2 = new User
            {
                Email = "sansa.stark@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
                Role = role,
                UserName = "SansaWinter"
            };
            var user3 = new User
            {
                Email = "moderator@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
                Role = moderRole,
                UserName = "moderator"
            };
            var user = new User
            {
                Email = "admin@gmail.com",
                Password = Crypto.HashPassword("qwerty"),
                Role = adminRole,
                UserName = "admin"
            };
            #endregion

            #region Profiles initializing
            var profile1 = new Profile
            {
                Age = 12,
                FirstName = "Arya",
                LastName = "Stark",
                UserName = user1.UserName
            };
            var profile2 = new Profile
            {
                Age = 15,
                FirstName = "Sansa",
                LastName = "Stark",
                UserName = user2.UserName
            };
            var profile3 = new Profile
            {
                UserName = user3.UserName
            };
            var profile = new Profile
            {
                UserName = user.UserName
            };

            context.Profiles.Add(profile1);
            context.Profiles.Add(profile2);
            context.Profiles.Add(profile3);
            context.Profiles.Add(profile);
            context.SaveChanges();
            #endregion

            user1.UserProfile = profile1;
            user2.UserProfile = profile2;
            user3.UserProfile = profile3;
            user.UserProfile = profile;

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}