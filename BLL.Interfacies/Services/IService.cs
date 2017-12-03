using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BLL.Interfacies.Entities;

namespace BLL.Interfacies.Services
{
    public interface IService<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAllEntities();
        T GetOneByPredicate(Expression<Func<T, bool>> predicates);
        IEnumerable<T> GetAllByPredicate(Expression<Func<T, bool>> predicates);
        void Create(T item);
        void Delete(T item);
        void Update(T item);
    }
}