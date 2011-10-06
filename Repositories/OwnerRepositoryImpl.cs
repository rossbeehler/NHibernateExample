using System.Collections.Generic;
using System.Linq;
using NHibernateExample.DataAccess;
using NHibernateExample.Models;

namespace NHibernateExample.Repositories
{
    public class OwnerRepositoryImpl : OwnerRepository
    {
        public PersistenceBroker PersistenceBroker { get; set; }

        public void Add(Owner owner)
        {
            PersistenceBroker.Create(owner);
        }

        public Owner Find(int ownerId)
        {
            return PersistenceBroker.Get<Owner>(ownerId);
        }

        public void Remove(Owner owner)
        {
            PersistenceBroker.Delete(owner);
        }
    }
}
