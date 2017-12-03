using System;

namespace DAL.Interfacies.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        //Rollback
    }
}