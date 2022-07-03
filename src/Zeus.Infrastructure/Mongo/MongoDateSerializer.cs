using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Zeus.Infrastructure.Mongo
{
   internal sealed class MongoDateSerializer : DateTimeSerializer
   {
      public override DateTime Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
      {
         return new(base.Deserialize(context, args).Ticks, DateTimeKind.Local);
      }

      public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, DateTime value)
      {
         base.Serialize(context, args, new(value.Ticks, DateTimeKind.Utc));
      }
   }
}
