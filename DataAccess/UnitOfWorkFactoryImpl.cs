namespace NHibernateExample.DataAccess
{
    public class UnitOfWorkFactoryImpl : UnitOfWorkFactory
    {
        public UnitOfWork StartUnitOfWork()
        {
            var unitOfWork = new UnitOfWorkImpl(SessionFactoryHelper.OpenSession());
            UnitOfWorkContext.Current = unitOfWork;
            return unitOfWork;
        }
    }
}
