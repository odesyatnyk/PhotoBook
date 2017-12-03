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
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository profileRepository;
        private readonly IUnitOfWork uow;

        public ProfileService(IUnitOfWork uow, IProfileRepository repository)
        {
            this.uow = uow;
            profileRepository = repository;
        }

        public ProfileEntity GetById(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException();
            var profile = profileRepository.GetById(id);
            if (ReferenceEquals(profile, null))
                return null;
            return profile.ToBllProfile();
        }

        public ProfileEntity GetProfileByName(string name)
        {
            var profile = profileRepository.GetProfileByName(name);
            if (ReferenceEquals(profile, null))
                return null;
            return profile.ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAllEntities()
        {
            return profileRepository.GetAll().Select(profile => profile.ToBllProfile());
        }


        public ProfileEntity GetOneByPredicate(Expression<Func<ProfileEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<ProfileEntity, DalProfile>(Expression.Parameter(typeof(DalProfile), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return profileRepository.GetOneByPredicate(exp2).ToBllProfile();
        }

        public IEnumerable<ProfileEntity> GetAllByPredicate(Expression<Func<ProfileEntity, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<ProfileEntity, DalProfile>(Expression.Parameter(typeof(DalProfile), predicates.Parameters[0].Name));
            var exp2 = Expression.Lambda<Func<DalProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameterExp);
            return profileRepository.GetAllByPredicate(exp2).Select(user => user.ToBllProfile()).ToList();
        }

        public void Create(ProfileEntity profile)
        {
            if (ReferenceEquals(profile, null))
                throw new ArgumentNullException();

            profileRepository.Create(profile.ToDalProfile());
            uow.Commit();
        }

        public void Delete(ProfileEntity profile)
        {
            if (ReferenceEquals(profile, null))
                throw new ArgumentNullException();

            profileRepository.Delete(profile.ToDalProfile());
            uow.Commit();
        }

        public void Update(ProfileEntity item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            profileRepository.Update(item.ToDalProfile());
            uow.Commit();
        }
    }
}