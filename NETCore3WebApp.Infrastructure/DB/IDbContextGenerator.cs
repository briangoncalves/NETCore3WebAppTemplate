using System;
using System.Collections.Generic;
using System.Text;

namespace NETCore3WebApp.Infrastructure.DB
{
    public interface IDbContextGenerator
    {
        IMyDbContext GenerateMyDbContext();
    }
}
