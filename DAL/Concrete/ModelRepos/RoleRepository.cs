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
    public class RoleRepository : IRoleRepository
    {
        private readonly UnitOfWork _unitOfWork; //not interface

        public RoleRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public IEnumerable<DalRole> GetAll()
        {
            return _unitOfWork.Context.Set<Role>().Select(role => role.ToDalRole());
        }

        public DalRole GetById(int key)
        {
            var orm = _unitOfWork.Context.Set<Role>().FirstOrDefault(role => role.RoleId == key);
            if (!ReferenceEquals(orm, null))
            {
                return orm.ToDalRole();
            }
            return null;
        }

        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalRole, Role>(Expression.Parameter(typeof(Role), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Role, bool>>(visitor.Visit(predicate.Body), visitor.NewParameterExp);
            var final = _unitOfWork.Context.Set<Role>().Where(express).ToList();
            return final.Select(role => role.ToDalRole());
        }

        public void Create(DalRole e)
        {
            var role = e.ToOrmRole();
            _unitOfWork.Context.Set<Role>().Add(role);
            _unitOfWork.Commit();
        }

        public void Delete(DalRole e)
        {
            var role = _unitOfWork.Context.Set<Role>().Single(u => u.RoleId == e.Id);
            _unitOfWork.Context.Set<Role>().Remove(role);
            _unitOfWork.Commit();
        }

        public void Update(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public DalRole GetRoleByName(string name)
        {
            var orm = _unitOfWork.Context.Set<Role>().FirstOrDefault(role => role.Name == name);
            if (!ReferenceEquals(orm, null))
            {
                return orm.ToDalRole();
            }
            return null;
        }
    }
}