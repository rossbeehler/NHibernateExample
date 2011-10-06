using System.Collections.Generic;
using System.Linq;
using NHibernateExample.DataAccess;
using NHibernateExample.Models;

namespace NHibernateExample.Repositories
{
    public class CatRepositoryImpl : CatRepository
    {
        public PersistenceBroker PersistenceBroker { get; set; }

        public void Add(Cat cat)
        {
            PersistenceBroker.Create(cat);
        }

        public Cat Find(int catId)
        {
            return PersistenceBroker.Get<Cat>(catId);
        }

        public IList<Cat> FindByOwnerName(string ownerName)
        {
            return PersistenceBroker.Query<Cat>()
                .Where(x => x.Owner.Name == ownerName)
                .ToList();
        }

        public void Remove(Cat cat)
        {
            PersistenceBroker.Delete(cat);
        }
    }
}
