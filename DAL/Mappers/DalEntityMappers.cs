using System.Linq;
using DAL.Interfacies.DTO;
using ORM.Models;

namespace DAL.Mappers
{
    public static class DalEntityMappers
    {
        #region User

        public static DalUser ToDalUser(this User userEntity)
        {
            return new DalUser
            {
                Id = userEntity.UserId,
                ProfileId = userEntity.UserProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId,
                Profile = userEntity.UserProfile.ToDalProfile(),
                UserName = userEntity.UserName
            };
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            return new User
            {
                UserId = dalUser.Id,
                UserProfileId = dalUser.ProfileId,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                Password = dalUser.Password,
                UserProfile = dalUser.Profile.ToOrmProfile(),
                UserName = dalUser.UserName
            };
        }

        #endregion

        #region Profile

        public static Profile ToOrmProfile(this DalProfile profile)
        {
            var newprofile = new Profile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Avatar = profile.Avatar,
                UserName = profile.UserName
            };
            foreach (var photo in profile.Photos)
            {
                newprofile.Photos.Add(photo.ToOrmPhoto());
            }
            return newprofile;
        }

        public static DalProfile ToDalProfile(this Profile profile)
        {
            var newprofile = new DalProfile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Avatar = profile.Avatar,
                UserName = profile.UserName
            };
            foreach (var photo in profile.Photos)
            {
                newprofile.Photos.Add(photo.ToDalPhoto());
            }
            return newprofile;
        }

        #endregion

        #region Role

        public static Role ToOrmRole(this DalRole role)
        {
            var orm = new Role
            {
                RoleId = role.Id,
                Name = role.Name
            };
            foreach (var item in role.Users)
            {
                orm.Users.Add(item.ToOrmUser());
            }
            return orm;
        }

        public static DalRole ToDalRole(this Role orm)
        {
            var role = new DalRole
            {
                Id = orm.RoleId,
                Name = orm.Name
            };
            foreach (var item in orm.Users)
            {
                role.Users.Add(item.ToDalUser());
            }
            return role;
        }

        #endregion

        #region Photo

        public static DalPhoto ToDalPhoto(this Photo photo)
        {
            return new DalPhoto
            {
                Id = photo.PhotoId,
                CreatedOn = photo.CreatedOn,
                Picture = photo.Picture,
                UserName = photo.UserName,
                FullSize = photo.FullSize,
                ProfileId = photo.ProfileId,
                Description = photo.Description,
                Likes = photo.Likes.Select(l => new DalLike
                {
                    Id = l.LikeId,
                    PhotoId = l.PhotoId,
                    UserName = l.UserName
                }).ToList(),
                Tags = photo.Tags.Select(t => new DalTag
                {
                    Id = t.TagId,
                    Name = t.Name
                }).ToList()
            };
        }

        public static Photo ToOrmPhoto(this DalPhoto dal)
        {
            return new Photo
            {
                PhotoId = dal.Id,
                CreatedOn = dal.CreatedOn,
                Picture = dal.Picture,
                FullSize = dal.FullSize,
                UserName = dal.UserName,
                ProfileId = dal.ProfileId,
                Description = dal.Description,
                Likes = dal.Likes.Select(l => new Like
                {
                    LikeId = l.Id,
                    PhotoId = l.PhotoId,
                    UserName = l.UserName
                }).ToList(),
                Tags = dal.Tags.Select(t => new Tag
                {
                    TagId = t.Id,
                    Name = t.Name
                }).ToList()
            };
        }

        #endregion

        #region Tag

        public static DalTag ToDalTag(this Tag tag)
        {
            var newtag = new DalTag
            {
                Id = tag.TagId,
                Name = tag.Name
            };
            foreach (var photo in tag.Photos)
            {
                newtag.Photos.Add(photo.ToDalPhoto());
            }
            return newtag;
        }

        public static Tag ToOrmTag(this DalTag tag)
        {
            var newtag = new Tag
            {
                TagId = tag.Id,
                Name = tag.Name
            };
            foreach (var photo in tag.Photos)
            {
                newtag.Photos.Add(photo.ToOrmPhoto());
            }
            return newtag;
        }

        #endregion

        #region Like

        public static Like ToOrmLike(this DalLike like)
        {
            return new Like
            {
                LikeId = like.Id,
                PhotoId = like.PhotoId,
                UserName = like.UserName
            };
        }

        public static DalLike ToDalLike(this Like like)
        {
            return new DalLike
            {
                Id = like.LikeId,
                PhotoId = like.PhotoId,
                UserName = like.UserName
            };
        }

        #endregion

        #region Exception Details

        public static DalExceptionDetail ToDalExceptionDetail(this ExceptionDetail exception)
        {
            return new DalExceptionDetail
            {
                ActionName = exception.ActionName,
                ControllerName = exception.ControllerName,
                Date = exception.Date,
                ExceptionMessage = exception.ExceptionMessage,
                Id = exception.Id,
                StackTrace = exception.StackTrace
            };
        }

        public static ExceptionDetail ToExceptionDetail(this DalExceptionDetail exception)
        {
            return new ExceptionDetail
            {
                ActionName = exception.ActionName,
                ControllerName = exception.ControllerName,
                Date = exception.Date,
                ExceptionMessage = exception.ExceptionMessage,
                Id = exception.Id,
                StackTrace = exception.StackTrace
            };
        }
        #endregion
    }
}