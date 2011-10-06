using System.Collections.Generic;
using NHibernateExample.DataAccess;
using NHibernateExample.Models;

namespace NHibernateExample.Repositories
{
    public interface CatRepository
    {
        void Add(Cat cat);

        Cat Find(int catId);
        IList<Cat> FindByOwnerName(string ownerName);

        void Remove(Cat cat);
    }
}
