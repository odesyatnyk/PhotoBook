using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Helper;
using DAL.Interfacies.Repository.ModelRepos;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete.ModelRepos
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly UnitOfWork _unitOfWork; 

        public ProfileRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalProfile> GetAll()
        {
            return _unitOfWork.Context.Set<Profile>().Select(profile => new DalProfile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Age = profile.Age,
                Avatar = profile.Avatar,
                Photos = profile.Photos.Select(p => new DalPhoto
                {
                    Id = p.PhotoId,
                    CreatedOn = p.CreatedOn,
                    Picture = p.Picture,
                    UserName = p.UserName,
                    Description = p.Description,
                    FullSize = p.FullSize,
                    Likes = p.Likes.Select(l => new DalLike
                    {
                        Id = l.LikeId,
                        PhotoId = l.PhotoId,
                        UserName = l.UserName
                    }).ToList(),
                    Tags = p.Tags.Select(t => new DalTag
                    {
                        Id = t.TagId,
                        Name = t.Name
                    }).ToList()
                }).ToList()
            });
        }

        public DalProfile GetById(int key)
        {
            var orm = _unitOfWork.Context.Set<Profile>().FirstOrDefault(profile => profile.Id == key);
            if (!ReferenceEquals(orm, null))
                return orm.ToDalProfile();
            return null;
        }

        public DalProfile GetProfileByName(string name)
        {
            var orm = _unitOfWork.Context.Set<Profile>().FirstOrDefault(profile => profile.UserName == name);
            if (!ReferenceEquals(orm, null))
            {
                return orm.ToDalProfile();
            }
            return null;
        }

        public void Create(DalProfile dalprofile)
        {
            var profile = dalprofile.ToOrmProfile();
            _unitOfWork.Context.Set<Profile>().Add(profile);
            _unitOfWork.Commit();
        }

        public void Delete(DalProfile dalprofile)
        {
            var profile = _unitOfWork.Context.Set<Profile>().Single(u => u.Id == dalprofile.Id);
            _unitOfWork.Context.Set<Profile>().Remove(profile);
            _unitOfWork.Commit();
        }

        public void Update(DalProfile entity)
        {
            var profile = _unitOfWork.Context.Set<Profile>().First(p => p.UserName == entity.UserName);
            _unitOfWork.Context.Set<Profile>().Attach(profile);
            if (entity.FirstName != null) profile.FirstName = entity.FirstName;
            if (entity.LastName != null) profile.LastName = entity.LastName;
            if (entity.Age != 0) profile.Age = entity.Age;
            if (entity.Avatar != null) profile.Avatar = entity.Avatar;
            foreach (var photo in entity.Photos)
            {
                photo.ProfileId = profile.Id;
                if (profile.Photos.FirstOrDefault(p => p.PhotoId == photo.Id) == null)
                {
                    var tags = photo.Tags.ToList();
                    photo.Tags.Clear();
                    var ormphoto = photo.ToOrmPhoto();

                    foreach (var tag in tags)
                    {
                        var dbtag = _unitOfWork.Context.Set<Tag>().FirstOrDefault(t => t.Name == tag.Name);
                        if (dbtag == null)
                        {
                            _unitOfWork.Context.Set<Tag>().Add(tag.ToOrmTag());
                            _unitOfWork.Commit();
                            dbtag = _unitOfWork.Context.Set<Tag>().FirstOrDefault(t => t.Name == tag.Name);
                        }
                        _unitOfWork.Context.Set<Tag>().Attach(dbtag);
                        dbtag.Photos.Add(ormphoto);
                        _unitOfWork.Commit();
                    }

                    photo.Tags = tags;
                }
            }
            _unitOfWork.Commit();
        }


        public DalProfile GetOneByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalProfile> GetAllByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalProfile, Profile>(Expression.Parameter(typeof(Profile), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Profile, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _unitOfWork.Context.Set<Profile>().Where(express).ToList();
            return final.Select(profile => profile.ToDalProfile());
        }
    }
}