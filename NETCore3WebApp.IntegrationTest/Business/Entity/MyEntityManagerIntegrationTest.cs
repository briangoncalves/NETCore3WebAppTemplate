using Microsoft.Extensions.Options;
using NUnit.Framework;
using NETCore3WebApp.Business;
using NETCore3WebApp.Infrastructure.DB;
using NETCore3WebApp.Domain;

namespace NETCore3WebApp.IntegrationTest.Business.Entity
{
    public class MyEntityManagerIntegrationTest
    {
        AppSettings settings;
        IOptions<AppSettings> options;
        [SetUp]
        public void Setup()
        {
            settings = TestHelper.GetApplicationConfiguration(TestContext.CurrentContext.TestDirectory);
            options = Options.Create(settings);
        }

        [Test]
        public void GetMyEntityShouldReturnEntity()
        {

            var contextGenerator = new DbContextGenerator(options);
            var manager = new MyEntityManager(contextGenerator);

            var entity = manager.GetMyEntity();

            Assert.NotNull(entity);
            Assert.AreEqual(1, entity.Id);
        }

        [Test]
        public void SaveMyEntityShouldReturnValueUpdated()
        {
            var contextGenerator = new DbContextGenerator(options);
            var manager = new MyEntityManager(contextGenerator);
            const string nameToUpdate = "INTEGRATION TEST";

            var entity = manager.GetMyEntity();
            var originalValue = entity.Name;
            entity.Name = nameToUpdate;
            manager.SaveMyEntity(entity);
            var entitySaved = manager.GetMyEntity();

            Assert.NotNull(entity);
            Assert.NotNull(entitySaved);
            Assert.AreEqual(entity.Name, entitySaved.Name);
            Assert.AreEqual(nameToUpdate, entitySaved.Name);

            entitySaved.Name = originalValue;
            manager.SaveMyEntity(entitySaved);
        }
    }
}