using System.Linq;
using NHibernate.Linq;

namespace NHibernateExample.DataAccess
{
    public class PersistenceBrokerImpl : PersistenceBroker
    {
        void PersistenceBroker.Create(object model)
        {
            ((UnitOfWorkImpl)UnitOfWorkContext.Current).session.Save(model);
        }

        T PersistenceBroker.Get<T>(object id)
        {
            return ((UnitOfWorkImpl)UnitOfWorkContext.Current).session.Get<T>(id);
        }

        IQueryable<T> PersistenceBroker.Query<T>()
        {
            return ((UnitOfWorkImpl)UnitOfWorkContext.Current).session.Linq<T>();
        }

        void PersistenceBroker.Delete(object model)
        {
            ((UnitOfWorkImpl)UnitOfWorkContext.Current).session.Delete(model);
        }
    }
}
