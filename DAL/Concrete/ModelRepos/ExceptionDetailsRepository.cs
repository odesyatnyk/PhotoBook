using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using DAL.Interfacies.DTO;
using DAL.Interfacies.Repository.ModelRepos;
using ORM.Models;

namespace DAL.Concrete.ModelRepos
{
    public class ExceptionDetailsRepository : IExceptionDetailsRepository
    {
        private readonly UnitOfWork _unitOfWork;

        public ExceptionDetailsRepository(UnitOfWork uow)
        {
            _unitOfWork = uow;
        }
        
        public void Create(DalExceptionDetail entity)
        {
            ExceptionDetail exceptionDetail = new ExceptionDetail
            {
                ExceptionMessage = entity.ExceptionMessage,
                ControllerName = entity.ControllerName,
                ActionName = entity.ActionName,
                StackTrace = entity.StackTrace,
                Date = entity.Date
            };

            _unitOfWork.Context.Entry(exceptionDetail).State = EntityState.Added;
        }

        #region Stabs
        public IEnumerable<DalExceptionDetail> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalExceptionDetail GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalExceptionDetail> GetAllByPredicate(Expression<Func<DalExceptionDetail, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public DalExceptionDetail GetOneByPredicate(Expression<Func<DalExceptionDetail, bool>> predicate)
        {
            throw new NotImplementedException();
        }
        
        public void Delete(DalExceptionDetail entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DalExceptionDetail entity)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
