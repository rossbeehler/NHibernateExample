using System;
using System.Data;

namespace NHibernateExample.DataAccess
{
    public interface UnitOfWork : IDisposable
    {
        void BeginTransaction(IsolationLevel isolationLevel);
        void Commit();
        void Rollback();

        void Flush();

        void Clear();
        void Evict(object model);
        void Refresh(object model);
    }
}
