using System;
using Autofac;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Zeus.Infrastructure.Mongo;
using Zeus.Infrastructure.Repositories;
using Zeus.Infrastructure.Settings;

namespace Zeus.Infrastructure.Configuration.Modules
{
   internal sealed class MongoModule : Module
   {
      public MongoModule()
      {
         BsonSerializer.RegisterSerializer(typeof(float), new SingleSerializer(BsonType.Double, new RepresentationConverter(false, true)));
         BsonSerializer.RegisterSerializer(typeof(DateTime), new MongoDateSerializer());
      }

      protected override void Load(ContainerBuilder builder)
      {
         builder.Register((MongoSettings settings) =>
         {
            return new MongoClient(new MongoClientSettings()
            {
               ReplicaSetName = settings.ReplicaSetName,
               Server = new MongoServerAddress(settings.Server, settings.Port)
            });
         })
         .As<IMongoClient>()
         .SingleInstance();

         builder.Register((MongoSettings settings, IMongoClient client) =>
         {
            return client.GetDatabase(settings.Database);
         })
         .As<IMongoDatabase>()
         .SingleInstance();

         builder.RegisterType<UnitOfWork>()
            .AsSelf()
            .InstancePerLifetimeScope();
      }
   }
}
