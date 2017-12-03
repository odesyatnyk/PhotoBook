using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;

namespace DAL.Interfacies.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int key);
        TEntity GetOneByPredicate(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAllByPredicate(Expression<Func<TEntity, bool>> predicate);
        void Create(TEntity e);
        void Delete(TEntity e);
        void Update(TEntity entity);
    }
}