using System;
using NHibernateExample.DataAccess;
using NHibernateExample.Repositories;
using Spring.Context.Support;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;

namespace NHibernateExample
{
    public class ObjectContainer
    {
        static GenericApplicationContext context = InitializeContext();

        private static GenericApplicationContext InitializeContext()
        {
            context = new GenericApplicationContext();

            Configure(typeof(UnitOfWorkFactory), typeof(UnitOfWorkFactoryImpl));
            Configure(typeof(PersistenceBroker), typeof(PersistenceBrokerImpl));
            Configure(typeof(CatRepository), typeof(CatRepositoryImpl));
            Configure(typeof(OwnerRepository), typeof(OwnerRepositoryImpl));

            return context;
        }

        private static void Configure(Type objectNameType, Type implementationType)
        {
            var builder = ObjectDefinitionBuilder.RootObjectDefinition(new DefaultObjectDefinitionFactory(), implementationType)
                .SetAutowireMode(AutoWiringMode.ByType)
                .SetSingleton(true);

            context.RegisterObjectDefinition(objectNameType.Name, builder.ObjectDefinition);
        }

        public static T Get<T>() where T : class
        {
            return context.GetObject(typeof(T).Name) as T;
        }
    }
}
