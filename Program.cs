using System;
using NHibernateExample.DataAccess;
using NHibernateExample.Models;
using NHibernateExample.Repositories;

namespace NHibernateExample
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var unitOfWorkFactory = ObjectContainer.Get<UnitOfWorkFactory>();
                var catRepository = ObjectContainer.Get<CatRepository>();

                using (var unitOfWork = unitOfWorkFactory.StartUnitOfWork())
                {
                    var garfield = new Cat
                    {
                        Name = "Garfield",
                        Gender = Gender.Male,
                        IsSpadeOrNeutered = false,
                    };

                    catRepository.Add(garfield);
                    unitOfWork.Flush();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }
    }
}
