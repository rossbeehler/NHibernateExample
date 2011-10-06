using System.Linq;

namespace NHibernateExample.DataAccess
{
    public interface PersistenceBroker
    {
        void Create(object model);

        T Get<T>(object id);
        IQueryable<T> Query<T>();

        void Delete(object model);
    }
}
