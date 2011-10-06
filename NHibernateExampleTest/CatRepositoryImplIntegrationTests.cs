using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernateExample.DataAccess;
using NHibernateExample.Repositories;
using NHibernateExample;
using NHibernateExample.Models;

namespace NHibernateExampleTest
{
    [TestClass]
    public class CatRepositoryImplIntegrationTests
    {
        private UnitOfWorkFactory unitOfWorkFactory;
        private CatRepository catRepository;
        private OwnerRepository ownerRepository;

        [TestInitialize]
        public void Setup()
        {
            log4net.Config.XmlConfigurator.Configure();
            unitOfWorkFactory = ObjectContainer.Get<UnitOfWorkFactory>();
            catRepository = ObjectContainer.Get<CatRepository>();
            ownerRepository = ObjectContainer.Get<OwnerRepository>();
        }

        [TestMethod]
        public void FindByOwnerName_CanExecute()
        {
            // Setup
            var garfield = new Cat 
            { 
                Name = "Garfield",
                Owner = new Owner { Name = "John Arbuckle" }
            };

            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                catRepository.Add(garfield);
                ownerRepository.Add(garfield.Owner);
                unitOfWork.Flush();
            }

            // Act and Assert
            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                var johnArbucklesCats = catRepository.FindByOwnerName("John Arbuckle");
                Assert.AreEqual(1, johnArbucklesCats.Count);
                Assert.AreEqual("Garfield", johnArbucklesCats[0].Name);
            }

            // Teardown
            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                garfield = catRepository.Find(garfield.Id);
                catRepository.Remove(garfield);
                ownerRepository.Remove(garfield.Owner);
                unitOfWork.Flush();
            }
        }
    }
}
