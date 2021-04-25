using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Persistence
{
    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            optionsBuilder.UseSqlServer("Server=.;Database=DGM_Identity_Db;Trusted_Connection=True;MultipleActiveResultSets=true;",
                sql => sql.MigrationsAssembly(typeof(ConfigurationDbContextFactory).GetTypeInfo().Assembly.GetName().Name));
            return new ConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
        }
    }
}
