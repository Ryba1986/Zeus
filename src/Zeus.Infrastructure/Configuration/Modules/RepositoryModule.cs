using System;
using System.IO;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Zeus.Infrastructure.Repositories;
using Zeus.Infrastructure.Settings;

namespace Zeus.Infrastructure.Configuration.Modules
{
   internal sealed class RepositoryModule : Module
   {
      protected override void Load(ContainerBuilder builder)
      {
         builder.Register((SqlSettings settings) =>
         {
            StringBuilder connectionBuilder = new();
            connectionBuilder.Append($"Server={settings.Server},{settings.Port};");
            connectionBuilder.Append($"Database={settings.Database};");
            connectionBuilder.Append($"User Id={settings.Username};");
            connectionBuilder.Append($"Password={settings.Password};");
            connectionBuilder.Append($"Encrypt={settings.Encrypt};");
            connectionBuilder.Append($"Command Timeout={settings.CommandTimeout};");

            DbContextOptions<UnitOfWork> contextOptions = new DbContextOptionsBuilder<UnitOfWork>()
               .UseSqlServer(connectionBuilder.ToString())
               .Options;

            UpdateDatabaseSchema(contextOptions);

            return contextOptions;
         })
         .AsSelf()
         .SingleInstance();

         builder
            .RegisterType<UnitOfWork>()
            .AsSelf()
            .InstancePerLifetimeScope();
      }

      private static void UpdateDatabaseSchema(DbContextOptions<UnitOfWork> contextOptions)
      {
         string sqlPath = $"{AppContext.BaseDirectory}Sql{Path.DirectorySeparatorChar}DatabaseSetup.sql";
         if (!File.Exists(sqlPath))
         {
            return;
         }

         using UnitOfWork uow = new(contextOptions);
         string query = File.ReadAllText(sqlPath);

         uow.Database.ExecuteSqlRaw(query);
         File.Delete(sqlPath);
      }
   }
}
