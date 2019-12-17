using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore3WebApp.Business
{
    public interface IMyEntityManager
    {
        Domain.Entity GetMyEntity();
        bool SaveMyEntity(Domain.Entity myEntity);
    }
}
