using FluentNHibernate.Cfg;
using NHibernate.Cfg;
using NHibernateExample.Models.Mappings;

namespace NHibernateExample.DataAccess
{
    public static class SessionFactoryHelper
    {
        private static NHibernate.ISessionFactory SessionFactory;

        static SessionFactoryHelper()
        {
            var nhConfigs = new Configuration();
            nhConfigs.SetProperty(Environment.Dialect, "NHibernate.Dialect.SQLiteDialect");
            nhConfigs.SetProperty(Environment.ConnectionDriver, "NHibernate.Driver.SQLite20Driver");
            nhConfigs.SetProperty(Environment.ConnectionString, "Data Source=NHibernateExample.db;Version=3;");
            nhConfigs.SetProperty(Environment.ProxyFactoryFactoryClass, "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle");
            nhConfigs.SetProperty(Environment.CommandTimeout, "600");

            SessionFactory = Fluently.Configure(nhConfigs)
                .Mappings(m => m.FluentMappings
                    .Add<CatMap>()
                    .Add<OwnerMap>()
                    .Add<ToyMap>()
                    .Conventions.Add<YesNoBoolPropertyConvention>())
                .BuildSessionFactory();
        }

        public static NHibernate.ISession OpenSession()
        {
            var session = SessionFactory.OpenSession();
            session.FlushMode = NHibernate.FlushMode.Commit;
            return session;
        }
    }
}
