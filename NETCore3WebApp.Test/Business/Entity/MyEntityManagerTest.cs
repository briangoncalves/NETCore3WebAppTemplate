using Moq;
using NETCore3WebApp.Business;
using NETCore3WebApp.Infrastructure.DB;
using NUnit.Framework;
using System.Collections.Generic;

namespace NETCore3WebApp.Test.Business.Entity
{
    public class MyEntityManagerTest
    {
        IDbContextGenerator contextGenerator;
        List<Domain.Entity> entities;
        [SetUp]
        public void Setup()
        {
            entities = new List<Domain.Entity>();
            entities.Add(new Domain.Entity
            {
                Id = 1,
                Name = "TEST NAME"
            });
            var myDbMoq = new Mock<IMyDbContext>();
            myDbMoq.Setup(p => p.Entities).Returns(DbContextMock.GetQueryableMockDbSet<Domain.Entity>(entities));
            myDbMoq.Setup(p => p.SaveChanges()).Returns(1);

            var moq = new Mock<IDbContextGenerator>();
            moq.Setup(p => p.GenerateMyDbContext()).Returns(myDbMoq.Object);

            contextGenerator = moq.Object;
        }

        [Test]
        public void GetConfigurationShouldReturnConfig()
        {
            var manager = new MyEntityManager(contextGenerator);

            var entity = manager.GetMyEntity();

            Assert.NotNull(entity);
            Assert.AreEqual(1, entity.Id);
            Assert.AreEqual("TEST NAME", entity.Name);
        }

        [Test]
        public void SaveConfigurationShouldReturnValueUpdated()
        {
            var manager = new MyEntityManager(contextGenerator);

            var entity = manager.GetMyEntity();
            entity.Name = "TEST UPDATE";
            manager.SaveMyEntity(entity);
            var entitySaved = manager.GetMyEntity();

            Assert.NotNull(entity);
            Assert.NotNull(entitySaved);
            Assert.AreEqual(entity.Name, entitySaved.Name);
            Assert.AreEqual("TEST UPDATE", entitySaved.Name);
        }
    }
}