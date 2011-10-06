using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using NHibernateExample.DataAccess;
using NHibernateExample.Models;

namespace NHibernateExampleTest
{
    [TestClass]
    public class ISessionIntegrationTests
    {
        [TestInitialize]
        public void Setup()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static object Insert(object o)
        {
            using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
            {
                return session.Save(o);
            }
        }

        private static void Delete<T>(object id)
        {
            using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
            {
                var model = session.Get<T>(id);
                if (model != null)
                {
                    session.Delete(model);
                }
                session.Flush();
            }
        }

        private static void PurchaseCat(object catId, string ownerName)
        {
            using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
            {
                var cat = session.Get<Cat>(catId);
                var owner = session.Linq<Owner>()
                    .First(x => x.Name == ownerName);

                cat.Owner = owner;
                cat.IsSpadeOrNeutered = true;

                session.Flush();
            }
        }

        [TestMethod]
        public void TestSaveExample()
        {
            using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
            {
                var garfield = new Cat
                {
                    Name = "Garfield",
                    Gender = Gender.Male,
                    IsSpadeOrNeutered = false,
                };

                session.Save(garfield);
                session.Flush();
            }

            using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
            {
                session.Linq<Cat>()
                    .Where(x => x.Name == "Garfield")
                    .ToList()
                    .ForEach(x => session.Delete(x));
                session.Flush();
            }
        }

        [TestMethod]
        public void TestReadUpdateExample()
        {
            var garfieldId = Insert(new Cat { Name = "Garfield" });
            var johnId = Insert(new Owner { Name = "John Arbuckle" });

            try
            {
                using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
                {
                    var garfield = session.Get<Cat>(garfieldId);
                    var johnArbuckle = session.Linq<Owner>()
                        .First(x => x.Name == "John Arbuckle");

                    garfield.Owner = johnArbuckle;
                    garfield.IsSpadeOrNeutered = true;

                    session.Flush();
                }
            }
            finally
            {
                Delete<Cat>(garfieldId);
                Delete<Owner>(johnId);
            }
        }

        [TestMethod]
        public void TestHQLDeleteExample()
        {
            var catId = Insert(new Cat { Name = "Garfield" });
            var ownerId = Insert(new Owner { Name = "John Arbuckle" });

            try
            {
                PurchaseCat(catId, "John Arbuckle");

                using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
                {
                    var johnArbucklesCats = session.CreateQuery(
                        "from Cat c where c.Owner.Name = :ownerName")
                        .SetString("ownerName", "John Arbuckle")
                        .List<Cat>();

                    foreach (var cat in johnArbucklesCats)
                    {
                        session.Delete(cat);
                    }

                    session.Flush();
                }
            }
            finally
            {
                Delete<Cat>(catId);
                Delete<Owner>(ownerId);
            }
        }

        [TestMethod]
        public void TestSaveGetAndDelete()
        {
            var garfieldId = Insert(new Cat { Name = "Garfield" });

            using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
            {
                var garfield = session.Get<Cat>(garfieldId);
                Assert.AreEqual("Garfield", garfield.Name);
            }

            Delete<Cat>(garfieldId);

            using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
            {
                var garfield = session.Get<Cat>(garfieldId);
                Assert.IsNull(garfield);
            }
        }

        [TestMethod]
        public void TestLinqAndUpdate()
        {
            var catId = Insert(new Cat { Name = "Garfield" });
            var johnId = Insert(new Owner { Name = "John Arbuckle" });

            try
            {
                PurchaseCat(catId, "John Arbuckle");

                using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
                {
                    var garfield = session.Get<Cat>(catId);
                    Assert.IsTrue(garfield.IsSpadeOrNeutered);
                    Assert.AreEqual("John Arbuckle", garfield.Owner.Name);
                }
            }
            finally
            {
                Delete<Cat>(catId);
                Delete<Owner>(johnId);
            }
        }

        [TestMethod]
        public void TestHQL()
        {
            var catId = Insert(new Cat { Name = "Garfield" });
            var ownerId = Insert(new Owner { Name = "John Arbuckle" });

            try
            {
                PurchaseCat(catId, "John Arbuckle");

                using (NHibernate.ISession session = SessionFactoryHelper.OpenSession())
                {
                    var johnArbucklesCats = session.CreateQuery(
                        "from Cat c where c.Owner.Name = :ownerName")
                        .SetString("ownerName", "John Arbuckle")
                        .List<Cat>();

                    Assert.AreEqual(1, johnArbucklesCats.Count);
                    Assert.AreEqual("Garfield", johnArbucklesCats[0].Name);
                }
            }
            finally
            {
                Delete<Cat>(catId);
                Delete<Owner>(ownerId);
            }
        }
    }
}
