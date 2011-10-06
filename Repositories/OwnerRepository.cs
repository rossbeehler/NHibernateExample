using System.Collections.Generic;
using NHibernateExample.DataAccess;
using NHibernateExample.Models;

namespace NHibernateExample.Repositories
{
    public interface OwnerRepository
    {
        void Add(Owner owner);

        Owner Find(int ownerId);

        void Remove(Owner owner);
    }
}
