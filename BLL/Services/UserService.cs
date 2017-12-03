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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork uow, IUserRepository repository)
        {
            this.uow = uow;
            userRepository = repository;
        }

        public UserEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            return userRepository.GetById(id).ToBllUser();
        }

        public UserEntity GetUserByEmail(string email)
        {
            var user = userRepository.GetByEmail(email);
            if (ReferenceEquals(user, null))
                return null;
            return user.ToBllUser();
        }

        public UserEntity GetUserByName(string name)
        {
            var user = userRepository.GetByName(name);
            if (ReferenceEquals(user, null))
                return null;
            return user.ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllEntities()
        {
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }


        public UserEntity GetOneByPredicate(Expression<Func<UserEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return userRepository.GetOneByPredicate(exp2).ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllByPredicate(Expression<Func<UserEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<UserEntity, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return userRepository.GetAllByPredicate(exp2).Select(user => user.ToBllUser()).ToList();
        }

        public void Create(UserEntity user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public void Delete(UserEntity user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void Update(UserEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            userRepository.Update(item.ToDalUser());
            uow.Commit();
        }
    }
}