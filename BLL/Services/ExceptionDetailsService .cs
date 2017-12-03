using BLL.Interfacies.Entities;
using BLL.Interfacies.Services;
using BLL.Mappers;
using DAL.Interfacies.Repository;
using DAL.Interfacies.Repository.ModelRepos;

namespace BLL.Services
{
    public class ExceptionDetailsService : IExceptionDetailsService
    {
        private readonly IExceptionDetailsRepository _exceptionrDetailsRepository;
        private readonly IUnitOfWork uow;

        public ExceptionDetailsService(IUnitOfWork uow, IExceptionDetailsRepository repository)
        {
            this.uow = uow;
            _exceptionrDetailsRepository = repository;
        }

        public void Create(ExceptionDetailsEntity entity)
        {
            _exceptionrDetailsRepository.Create(entity.ToDalExceptionDetail());
            uow.Commit();
        }
    }
}
