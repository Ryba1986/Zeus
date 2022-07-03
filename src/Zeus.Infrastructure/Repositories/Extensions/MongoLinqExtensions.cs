using AutoMapper;
using AutoMapper.QueryableExtensions;
using MongoDB.Driver.Linq;

namespace Zeus.Infrastructure.Repositories.Extensions
{
   internal static class MongoLinqExtensions
   {
      public static IMongoQueryable<T> ProjectTo<T>(this IMongoQueryable query, IMapper mapper)
      {
         return (IMongoQueryable<T>)query.ProjectTo<T>(mapper.ConfigurationProvider);
      }
   }
}
