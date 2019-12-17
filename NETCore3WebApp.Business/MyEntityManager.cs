using NETCore3WebApp.Infrastructure.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NETCore3WebApp.Business
{
    public class MyEntityManager : IMyEntityManager
    {
        private readonly IDbContextGenerator _contextGenerator;
        public MyEntityManager(IDbContextGenerator contextGenerator)
        {
            _contextGenerator = contextGenerator;
        }

        public Domain.Entity GetMyEntity()
        {
            var configuration = _contextGenerator.GenerateMyDbContext().Entities.FirstOrDefault();
            return configuration;
        }

        public bool SaveMyEntity(Domain.Entity myEntity)
        {
            using (var context = _contextGenerator.GenerateMyDbContext())
            {
                var entityToUpdate = context.Entities.FirstOrDefault();
                if (entityToUpdate == null)
                {
                    entityToUpdate = new Domain.Entity();
                    context.Entities.Add(entityToUpdate);
                }
                PropertiesHelper.CopyProperties(myEntity, entityToUpdate);
                context.SaveChanges();
            }
            return true;
        }
    }
}
