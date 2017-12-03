using System.Linq;
using BLL.Interfacies.Entities;
using DAL.Interfacies.DTO;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        #region User

        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser
            {
                Id = userEntity.Id,
                ProfileId = userEntity.ProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId,
                Profile = userEntity.Profile.ToDalProfile(),
                UserName = userEntity.UserName
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            if (dalUser == null)
            {
                return null;
            }
            return new UserEntity
            {
                Id = dalUser.Id,
                ProfileId = dalUser.ProfileId,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                Password = dalUser.Password,
                Profile = dalUser.Profile.ToBllProfile(),
                UserName = dalUser.UserName
            };
        }

        #endregion

        #region Profile

        public static DalProfile ToDalProfile(this ProfileEntity profileEntity)
        {
            var profile = new DalProfile
            {
                Id = profileEntity.Id,
                FirstName = profileEntity.FirstName,
                LastName = profileEntity.LastName,
                Age = profileEntity.Age,
                Avatar = profileEntity.Avatar,
                UserName = profileEntity.UserName
            };
            foreach (var photo in profileEntity.Photos)
            {
                profile.Photos.Add(photo.ToDalPhoto());
            }
            return profile;
        }

        public static ProfileEntity ToBllProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null) return null;
            var profile = new ProfileEntity
            {
                Id = dalProfile.Id,
                FirstName = dalProfile.FirstName,
                LastName = dalProfile.LastName,
                Age = dalProfile.Age,
                Avatar = dalProfile.Avatar,
                UserName = dalProfile.UserName
            };
            foreach (var photo in dalProfile.Photos)
            {
                profile.Photos.Add(photo.ToBllPhoto());
            }
            return profile;
        }

        #endregion

        #region Like

        public static LikeEntity ToBllLike(this DalLike like)
        {
            return new LikeEntity
            {
                Id = like.Id,
                PhotoId = like.PhotoId,
                UserName = like.UserName
            };
        }

        public static DalLike ToDalLike(this LikeEntity like)
        {
            return new DalLike
            {
                Id = like.Id,
                PhotoId = like.PhotoId,
                UserName = like.UserName
            };
        }

        #endregion

        #region Tag

        public static TagEntity ToBllTag(this DalTag tag)
        {
            var newtag = new TagEntity
            {
                Id = tag.Id,
                Name = tag.Name
            };
            foreach (var photo in tag.Photos)
            {
                newtag.Photos.Add(photo.ToBllPhoto());
            }
            return newtag;
        }

        public static DalTag ToDalTag(this TagEntity tag)
        {
            var newtag = new DalTag
            {
                Id = tag.Id,
                Name = tag.Name
            };
            foreach (var photo in tag.Photos)
            {
                newtag.Photos.Add(photo.ToDalPhoto());
            }
            return newtag;
        }

        #endregion

        #region Photo

        public static PhotoEntity ToBllPhoto(this DalPhoto photo)
        {
            return new PhotoEntity
            {
                CreatedOn = photo.CreatedOn,
                FullSize = photo.FullSize,
                Description = photo.Description,
                Id = photo.Id,
                UserName = photo.UserName,
                Picture = photo.Picture,
                ProfileId = photo.ProfileId,
                Likes = photo.Likes.Select(l => new LikeEntity
                {
                    Id = l.Id,
                    PhotoId = l.PhotoId,
                    UserName = l.UserName
                }).ToList(),
                Tags = photo.Tags.Select(t => new TagEntity
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            };
        }

        public static DalPhoto ToDalPhoto(this PhotoEntity photo)
        {
            return new DalPhoto
            {
                CreatedOn = photo.CreatedOn,
                FullSize = photo.FullSize,
                ProfileId = photo.ProfileId,
                UserName = photo.UserName,
                Description = photo.Description,
                Id = photo.Id,
                Picture = photo.Picture,
                Likes = photo.Likes.Select(l => new DalLike
                {
                    Id = l.Id,
                    PhotoId = l.PhotoId,
                    UserName = l.UserName
                }).ToList(),
                Tags = photo.Tags.Select(t => new DalTag
                {
                    Id = t.Id,
                    Name = t.Name
                }).ToList()
            };
        }

        #endregion

        #region Role

        public static RoleEntity ToBllRole(this DalRole role)
        {
            var roleEntity = new RoleEntity
            {
                Id = role.Id,
                Name = role.Name
            };
            foreach (var item in role.Users)
            {
                roleEntity.Users.Add(item.ToBllUser());
            }
            return roleEntity;
        }

        public static DalRole ToBllRole(this RoleEntity role)
        {
            var dalRole = new DalRole
            {
                Id = role.Id,
                Name = role.Name
            };
            foreach (var item in role.Users)
            {
                dalRole.Users.Add(item.ToDalUser());
            }
            return dalRole;
        }

        #endregion

        #region Exception Details

        public static DalExceptionDetail ToDalExceptionDetail(this ExceptionDetailsEntity exception)
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

        public static ExceptionDetailsEntity ToBllExceptionDetail(this DalExceptionDetail exception)
        {
            return new ExceptionDetailsEntity
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