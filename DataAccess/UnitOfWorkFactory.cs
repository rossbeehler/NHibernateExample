namespace NHibernateExample.DataAccess
{
    public interface UnitOfWorkFactory
    {
        UnitOfWork StartUnitOfWork();
    }
}
