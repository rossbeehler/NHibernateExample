using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernateExample;
using NHibernateExample.DataAccess;
using NHibernateExample.Models;
using NHibernateExample.Repositories;

namespace NHibernateExampleTest
{
    [TestClass]
    public class UnitOfWorkIntegrationTests
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

        private int InsertCat(Cat cat)
        {
            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                catRepository.Add(cat);
                return cat.Id;
            }
        }

        private int InsertOwner(Owner owner)
        {
            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                ownerRepository.Add(owner);
                return owner.Id;
            }
        }

        private void DeleteCat(int catId)
        {
            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                var cat = catRepository.Find(catId);
                catRepository.Remove(cat);
                unitOfWork.Flush();
            }
        }

        private void DeleteOwner(int ownerId)
        {
            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                var owner = ownerRepository.Find(ownerId);
                ownerRepository.Remove(owner);
                unitOfWork.Flush();
            }
        }

        [TestMethod]
        public void TestAddFindAndRemove()
        {
            var catId = InsertCat(new Cat { Name = "Garfield" });

            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                var cat = catRepository.Find(catId);
                Assert.AreEqual("Garfield", cat.Name);
            }

            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                var cat = catRepository.Find(catId);
                catRepository.Remove(cat);
                unitOfWork.Flush();
            }

            using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
            {
                var cat = catRepository.Find(catId);
                Assert.IsNull(cat);
            }
        }

        [TestMethod]
        public void TestFindByUserName()
        {
            var catId = InsertCat(new Cat { Name = "Garfield" });
            var ownerId = InsertOwner(new Owner { Name = "John Arbuckle" });

            try
            {
                using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
                {
                    var cat = catRepository.Find(catId);
                    var owner = ownerRepository.Find(ownerId);

                    cat.Owner = owner;
                    cat.IsSpadeOrNeutered = true;

                    unitOfWork.Flush();
                }

                using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
                {
                    var johnArbucklesCats = catRepository.FindByOwnerName("John Arbuckle");
                    Assert.AreEqual(1, johnArbucklesCats.Count);
                    Assert.AreEqual("Garfield", johnArbucklesCats[0].Name);
                }
            }
            finally
            {
                DeleteCat(catId);
                DeleteOwner(ownerId);
            }
        }
    }
}
