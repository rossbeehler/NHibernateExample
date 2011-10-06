using System.Data;

namespace NHibernateExample.DataAccess
{
    public class UnitOfWorkImpl : UnitOfWork
    {
        internal NHibernate.ISession session;

        public UnitOfWorkImpl(NHibernate.ISession session)
        {
            session.FlushMode = NHibernate.FlushMode.Commit;
            this.session = session;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            session.BeginTransaction();
        }

        public void Commit()
        {
            session.Transaction.Commit();
        }

        public void Rollback()
        {
            session.Transaction.Rollback();
        }

        public void Flush()
        {
            session.Flush();
        }

        public void Clear()
        {
            session.Clear();
        }

        public void Evict(object model)
        {
            session.Evict(model);
        }

        public void Refresh(object model)
        {
            session.Refresh(model);
        }

        public void Dispose()
        {
            if (session.Transaction != null &&
                session.Transaction.IsActive &&
                !session.Transaction.WasCommitted)
            {
                this.Rollback();
            }

            session.Dispose();
        }
    }
}
