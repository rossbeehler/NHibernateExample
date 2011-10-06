using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateExample.DataAccess
{
    internal static class UnitOfWorkContext
    {
        [ThreadStatic]
        private static UnitOfWork unitOfWork;

        internal static UnitOfWork Current 
        { 
            get { return unitOfWork; } 
            set { unitOfWork = value; } 
        }
    }
}
