using System.Collections.Generic;
using BLL.Interfacies.Entities;
using MvcPL.Models;
using MvcPL.Models.Photo;
using MvcPL.Models.Profile;
using MvcPL.Models.User;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        #region User

        public static UserViewModel ToMvcUser(this UserEntity user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                Name = user.UserName,
                Id = user.Id
            };
        }

        public static UserEntity ToUserEntity(this UserViewModel userViewModel)
        {
            return new UserEntity
            {
                Email = userViewModel.Email,
                UserName = userViewModel.Name,
                Id = userViewModel.Id
            };
        }

        #endregion

        #region Tag

        public static ICollection<TagEntity> ToTagEntitys(this ICollection<TagModel> tags)
        {
            var result = new List<TagEntity>();
            foreach (var tag in tags)
            {
                result.Add(new TagEntity
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return result;
        }

        public static ICollection<TagModel> ToMvcTags(this ICollection<TagEntity> tags)
        {
            var result = new List<TagModel>();
            foreach (var tag in tags)
            {
                result.Add(new TagModel
                {
                    Id = tag.Id,
                    Name = tag.Name
                });
            }
            return result;
        }

        #endregion

        #region Like

        public static ICollection<LikeEntity> ToLikeEntitys(this ICollection<LikeModel> likes)
        {
            var result = new List<LikeEntity>();
            foreach (var like in likes)
            {
                result.Add(new LikeEntity
                {
                    Id = like.Id,
                    UserName = like.UserName,
                    PhotoId = like.PhotoId
                });
            }
            return result;
        }

        public static ICollection<LikeModel> ToMvcLikes(this ICollection<LikeEntity> likes)
        {
            var result = new List<LikeModel>();
            foreach (var like in likes)
            {
                result.Add(new LikeModel
                {
                    Id = like.Id,
                    UserName = like.UserName,
                    PhotoId = like.PhotoId
                });
            }
            return result;
        }

        #endregion

        #region Photo

        public static PhotoEntity ToPhotoEntity(this PhotoViewModel photo)
        {
            return new PhotoEntity
            {
                Id = photo.Id,
                CreatedOn = photo.CreatedOn,
                Picture = photo.Picture,
                FullSize = photo.FullSize,
                UserName = photo.UserName,
                Description = photo.Description,
                Tags = photo.Tags.ToTagEntitys(),
                Likes = photo.Likes.ToLikeEntitys()
            };
        }

        public static PhotoViewModel ToMvcPhoto(this PhotoEntity photo)
        {
            return new PhotoViewModel
            {
                Id = photo.Id,
                CreatedOn = photo.CreatedOn,
                Picture = photo.Picture,
                FullSize = photo.FullSize,
                Description = photo.Description,
                UserName = photo.UserName,
                Tags = photo.Tags.ToMvcTags(),
                Likes = new List<LikeModel>(photo.Likes.ToMvcLikes())
            };
        }

        #endregion

        #region Profile

        public static ProfileEntity ToProfileEntity(this ProfileViewModel model)
        {
            return new ProfileEntity
            {
                Age = model.Age,
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Avatar = model.Avatar,
                UserName = model.UserName
            };
        }

        public static ProfileViewModel ToMvcProfile(this ProfileEntity model)
        {
            return new ProfileViewModel
            {
                Age = model.Age,
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Avatar = model.Avatar,
                UserName = model.UserName
            };
        }

        #endregion
    }
}