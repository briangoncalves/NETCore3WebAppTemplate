using Microsoft.EntityFrameworkCore;
using System;

namespace NETCore3WebApp.Infrastructure.DB
{
    public interface IMyDbContext : IDisposable
    {
        int SaveChanges();
        DbSet<Domain.Entity> Entities { get; set; }
    }
}
