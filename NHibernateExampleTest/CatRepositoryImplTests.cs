using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernateExample.Repositories;
using Rhino.Mocks;
using NHibernateExample.DataAccess;
using NHibernateExample.Models;

namespace NHibernateExampleTest
{
    [TestClass]
    public class CatRepositoryImplTests
    {
        CatRepositoryImpl repository;

        [TestInitialize]
        public void Setup()
        {
            log4net.Config.XmlConfigurator.Configure();
            repository = new CatRepositoryImpl
            {
                PersistenceBroker = MockRepository.GenerateStub<PersistenceBroker>()
            };
        }

        [TestMethod]
        public void FindByOwnerName_NoResults_EmptyListReturned()
        {
            var cats = new List<Cat>();

            repository.PersistenceBroker.Expect(x => x.Query<Cat>())
                .IgnoreArguments()
                .Return(cats.AsQueryable());

            var results = repository.FindByOwnerName("a");

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void FindByOwnerName_HasMatch_MatchReturned()
        {
            var expectedCat = new Cat { Owner = new Owner { Name = "a" } };
            var cats = new List<Cat>()
            {
                expectedCat,
            };

            repository.PersistenceBroker.Expect(x => x.Query<Cat>())
                .IgnoreArguments()
                .Return(cats.AsQueryable());

            var results = repository.FindByOwnerName("a");

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual(expectedCat, results[0]);
        }

        [TestMethod]
        public void FindByOwnerName_HasMultipleMatches_MatchesReturned()
        {
            var cats = new List<Cat>()
            {
                new Cat { Owner = new Owner { Name = "a" } },
                new Cat { Owner = new Owner { Name = "b" } },
                new Cat { Owner = new Owner { Name = "a" } },
                new Cat { Owner = new Owner { Name = "c" } },
            };

            var repository = new CatRepositoryImpl
            {
                PersistenceBroker = MockRepository.GenerateStub<PersistenceBroker>()
            };

            repository.PersistenceBroker
                .Expect(x => x.Query<Cat>())
                .Return(cats.AsQueryable());

            var results = repository.FindByOwnerName("a");

            Assert.AreEqual(2, results.Count);
            Assert.AreEqual("a", results[0].Owner.Name);
            Assert.AreEqual("a", results[1].Owner.Name);
        }
    }
}
