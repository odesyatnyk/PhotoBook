using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Helper;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Repository.ModelRepos;

namespace BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork uow;

        public TagService(IUnitOfWork uow, ITagRepository repository)
        {
            this.uow = uow;
            tagRepository = repository;
        }


        public TagEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            var tag = tagRepository.GetById(id);
            if (ReferenceEquals(tag, null))
                return null;
            return tag.ToBllTag();
        }

        public IEnumerable<TagEntity> GetAllEntities()
        {
            return tagRepository.GetAll().Select(tag => tag.ToBllTag());
        }


        public TagEntity GetOneByPredicate(Expression<Func<TagEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<TagEntity, DalTag>(Expression.Parameter(typeof(DalTag), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalTag, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return tagRepository.GetOneByPredicate(exp2).ToBllTag();
        }

        public IEnumerable<TagEntity> GetAllByPredicate(Expression<Func<TagEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<TagEntity, DalTag>(Expression.Parameter(typeof(DalTag), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalTag, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return tagRepository.GetAllByPredicate(exp2).Select(user => user.ToBllTag()).ToList();
        }

        public void Create(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Create(item.ToDalTag());
            uow.Commit();
        }

        public void Delete(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Delete(item.ToDalTag());
            uow.Commit();
        }

        public void Update(TagEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();
            tagRepository.Update(item.ToDalTag());
            uow.Commit();
        }

        public TagEntity GetTagByName(string name)
        {
            var tag = tagRepository.GetTagByName(name);
            if (ReferenceEquals(tag, null))
                return null;
            return tag.ToBllTag();
        }
    }
}
