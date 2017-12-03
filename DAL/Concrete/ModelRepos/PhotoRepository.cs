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
    public class PhotoRepository : IPhotoRepository
    {
        private readonly UnitOfWork _unitOfWork; //not interface

        public PhotoRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalPhoto> GetAll()
        {
            return _unitOfWork.Context.Set<Photo>().Select(photo => new DalPhoto
            {
                Id = photo.PhotoId,
                CreatedOn = photo.CreatedOn,
                Picture = photo.Picture,
                FullSize = photo.FullSize,
                ProfileId = photo.ProfileId,
                Description = photo.Description,
                UserName = photo.UserName,
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
            });
        }

        public DalPhoto GetById(int key)
        {
            var orm = _unitOfWork.Context.Set<Photo>().FirstOrDefault(photo => photo.PhotoId == key);
            if (!ReferenceEquals(orm, null))
                return orm.ToDalPhoto();
            return null;
        }

        public DalPhoto GetOneByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalPhoto> GetAllByPredicate(Expression<Func<DalPhoto, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalPhoto, Photo>(Expression.Parameter(typeof(Photo), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Photo, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _unitOfWork.Context.Set<Photo>().Where(express).ToList();
            return final.Select(photo => photo.ToDalPhoto());
        }
        
        public void Create(DalPhoto e)
        {
            var photo = e.ToOrmPhoto();
            _unitOfWork.Context.Set<Photo>().Add(photo);
            _unitOfWork.Commit();
        }

        public void Delete(DalPhoto e)
        {
            var photo = _unitOfWork.Context.Set<Photo>().Single(u => u.PhotoId == e.Id);
            _unitOfWork.Context.Set<Photo>().Remove(photo);
            _unitOfWork.Commit();
        }

        public void Update(DalPhoto photo)
        {
            foreach (var like in photo.Likes)
            {
                _unitOfWork.Context.Set<Like>().AddOrUpdate(like.ToOrmLike());
            }
            _unitOfWork.Context.Set<Photo>().AddOrUpdate(photo.ToOrmPhoto());
            _unitOfWork.Commit();
        }

        public void RemoveLike(DalLike like)
        {
            var photo = _unitOfWork.Context.Set<Photo>().First(p => p.PhotoId == like.PhotoId);
            photo.Likes.Remove(like.ToOrmLike());
            var dblike =
                _unitOfWork.Context.Set<Like>().First(l => l.UserName == like.UserName && l.PhotoId == like.PhotoId);
            _unitOfWork.Context.Set<Like>().Remove(dblike);
            _unitOfWork.Commit();
        }

        public void AddLike(DalLike like)
        {
            var photo = _unitOfWork.Context.Set<Photo>().First(p => p.PhotoId == like.PhotoId);
            _unitOfWork.Context.Set<Photo>().Attach(photo);
            photo.Likes.Add(like.ToOrmLike());
            _unitOfWork.Commit();
        }
    }
}