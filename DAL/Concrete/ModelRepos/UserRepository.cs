using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Helper;
using DAL.Interfacies.Repository.ModelRepos;
using DAL.Mappers;
using ORM.Models;

namespace DAL.Concrete.ModelRepos
{
    public class UserRepository : IUserRepository
    {
        private readonly UnitOfWork _unitOfWork; //not interface

        public UserRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalUser> GetAll()
        {
            return _unitOfWork.Context.Set<User>().Select(userEntity => new DalUser
            {
                Id = userEntity.UserId,
                ProfileId = userEntity.UserProfileId,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.RoleId,
                UserName = userEntity.UserName,
                Profile = new DalProfile
                {
                    Age = userEntity.UserProfile.Age,
                    Avatar = userEntity.UserProfile.Avatar,
                    FirstName = userEntity.UserProfile.FirstName,
                    LastName = userEntity.UserProfile.LastName,
                    Id = userEntity.UserProfile.Id,
                    UserName = userEntity.UserProfile.UserName,
                    Photos = userEntity.UserProfile.Photos.Select(p => new DalPhoto
                    {
                        Id = p.PhotoId,
                        CreatedOn = p.CreatedOn,
                        Picture = p.Picture,
                        FullSize = p.FullSize,
                        ProfileId = p.ProfileId,
                        Description = p.Description,
                        UserName = p.UserName,
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
                }
            });
        }

        public DalUser GetById(int key)
        {
            var ormuser = _unitOfWork.Context.Set<User>().FirstOrDefault(user => user.UserId == key);
            if (!ReferenceEquals(ormuser, null))
                return ormuser.ToDalUser();
            return null;
        }
        
        public DalUser GetByEmail(string email)
        {
            var ormuser = _unitOfWork.Context.Set<User>().FirstOrDefault(user => user.Email == email);
            if (!ReferenceEquals(ormuser, null))
            {
                return ormuser.ToDalUser();
            }
            return null;
        }

        public DalUser GetByName(string name)
        {
            var ormuser = _unitOfWork.Context.Set<User>().FirstOrDefault(user => user.UserName == name);
            if (!ReferenceEquals(ormuser, null))
            {
                return ormuser.ToDalUser();
            }
            return null;
        }


        public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalUser, User>(Expression.Parameter(typeof(User), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<User, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _unitOfWork.Context.Set<User>().Where(express).ToList();
            return final.Select(user => user.ToDalUser());
        }

        public void Create(DalUser e)
        {
            var profile = new DalProfile
            {
                UserName = e.UserName
            };
            e.Profile = profile;

            var user = e.ToOrmUser();
            user.Role = _unitOfWork.Context.Set<Role>().Find(e.RoleId);

            _unitOfWork.Context.Set<User>().Add(user);
            _unitOfWork.Commit();

            var pr = _unitOfWork.Context.Set<Profile>().First(p => p.UserName == user.UserName);
            user.UserProfileId = pr.Id;
            _unitOfWork.Context.Set<User>().AddOrUpdate(user);

            _unitOfWork.Commit();
        }

        public void Delete(DalUser e)
        {
            var user = _unitOfWork.Context.Set<User>().Single(u => u.UserId == e.Id);
            _unitOfWork.Context.Set<Photo>().RemoveRange(user.UserProfile.Photos);
            _unitOfWork.Context.Set<Profile>().Remove(user.UserProfile);
            _unitOfWork.Context.Set<User>().Remove(user);
            _unitOfWork.Commit();
        }

        public void Update(DalUser entity)
        {
            _unitOfWork.Context.Set<User>().AddOrUpdate(entity.ToOrmUser());
            _unitOfWork.Commit();
        }
    }
}